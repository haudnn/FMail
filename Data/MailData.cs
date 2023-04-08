using Workdo.Helpers;
using MongoDB.Driver;
using Workdo.Models;
using MongoDB.Bson;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Workdo.Data;



public class MailData 
{

    private static IMongoClient _client = ConnectDB.GetClient();
    private static IMongoDatabase database = _client.GetDatabase("mailbox");
    private static IMongoCollection<MailModel> mailCollection = database.GetCollection<MailModel>("mail");
    private static IMongoCollection<LabelModel> labelCollection = database.GetCollection<LabelModel>("label");

    public static async Task<List<MailModel>> GetAllMailsByFolder(string folderName, string userid)
    {
        List<MailModel> mails = new List<MailModel>();
        if(folderName == "trash")
        {
            var filter = Builders<MailModel>.Filter.And(
                    Builders<MailModel>.Filter.Eq(x => x.isTrash, true),
                    Builders<MailModel>.Filter.Eq(x => x.isDeleted, false),
                    Builders<MailModel>.Filter.Eq(x => x.folder, folderName),
                    Builders<MailModel>.Filter.Or(
                        Builders<MailModel>.Filter.Eq(x => x.author, userid),
                        Builders<MailModel>.Filter.Where(x => x.to.Any(t => t.id == userid)),
                        Builders<MailModel>.Filter.Where(x => x.cc.Any(c => c.id == userid)),
                        Builders<MailModel>.Filter.Where(x => x.bcc.Any(b => b.id == userid))
                    )
                );
            mails = await mailCollection.Find(filter).ToListAsync();
            mails = mails.GroupBy(m => m.originalMailId ?? m.id).Select(g => g.First()).ToList();
            return mails;
        }

        if(folderName == "sent")
        {
            var filter = Builders<MailModel>.Filter.And(
                Builders<MailModel>.Filter.Eq(x => x.isTrash, false),
                Builders<MailModel>.Filter.Eq(x => x.isDeleted, false),
                Builders<MailModel>.Filter.Eq(x => x.folder, folderName),
                Builders<MailModel>.Filter.Or(
                    Builders<MailModel>.Filter.Eq(x => x.author, userid),
                    Builders<MailModel>.Filter.Eq(x => x.isImportant, true)
                )
            );
            mails = await mailCollection.Find(filter).ToListAsync();
            return mails;
        }


        if(folderName == "important")
        {
            var filter = Builders<MailModel>.Filter.And(
                Builders<MailModel>.Filter.Eq(x => x.isTrash, false),
                Builders<MailModel>.Filter.Eq(x => x.isDeleted, false),
                Builders<MailModel>.Filter.Eq(x => x.isImportant, true),
                Builders<MailModel>.Filter.Or(
                        Builders<MailModel>.Filter.Eq(x => x.author, userid),
                        Builders<MailModel>.Filter.Where(x => x.to.Any(t => t.id == userid)),
                        Builders<MailModel>.Filter.Where(x => x.cc.Any(c => c.id == userid)),
                        Builders<MailModel>.Filter.Where(x => x.bcc.Any(b => b.id == userid))
                    )
            );
            mails = await mailCollection.Find(filter).ToListAsync();
            return mails;
        }

        
        if(folderName == "inbox")
        {
            var filter = Builders<MailModel>.Filter.And(
                Builders<MailModel>.Filter.Eq(x => x.isTrash, false),
                Builders<MailModel>.Filter.Eq(x => x.isDeleted, false),
                Builders<MailModel>.Filter.Eq(x => x.folder, folderName),
                Builders<MailModel>.Filter.Or(
                    Builders<MailModel>.Filter.Where(x => x.to.Any(t => t.id == userid)),
                    Builders<MailModel>.Filter.Where(x => x.cc.Any(c => c.id == userid)),
                    Builders<MailModel>.Filter.Where(x => x.bcc.Any(b => b.id == userid))
                )
            );
            mails = await mailCollection.Find(filter).ToListAsync();
            mails = mails.GroupBy(m => m.originalMailId ?? m.id).Select(g => g.First()).ToList();
            return mails;
        }

        if (folderName == "draft")
        {
            var filter = Builders<MailModel>.Filter.And(
                Builders<MailModel>.Filter.Eq(x => x.isDraft, true),
                Builders<MailModel>.Filter.Eq(x => x.isTrash, false),
                Builders<MailModel>.Filter.Eq(x => x.isDeleted, false),
                Builders<MailModel>.Filter.Eq(x => x.folder, folderName),
                Builders<MailModel>.Filter.Eq(x => x.author, userid)
            );
            mails = await mailCollection.Find(filter).ToListAsync();
            // mails = mails.GroupBy(m => m.originalMailId ?? m.id).Select(g => g.First()).ToList();
            return mails;
        }
        return mails;
    }


    public static async Task<MailModel> GetMailById(string id) 
    {
        MailModel mail = new MailModel();
        var isFoundMail = await mailCollection.Find(x => x.id == id).FirstOrDefaultAsync();
        if (isFoundMail != null)
        {
            mail = isFoundMail;
            if(isFoundMail.originalMailId != null) 
            {
                var isFoundOriginalMail = await mailCollection.Find(x => x.id == isFoundMail.originalMailId).FirstOrDefaultAsync();
                if(isFoundOriginalMail!= null) 
                {
                    mail.to = isFoundOriginalMail.to;
                    mail.cc = isFoundOriginalMail.cc;
                }
            }
        }
        return mail;
    }

    public static async Task CreateMail(MailModel mail)
    {
        var id = GenerateIDHelper.GenerateID("19012001");
        mail.id = id;
        mail.isDeleted = false;
        mail.isRead = false;
        mail.isReply = false;
        mail.replies = new List<string>();
        mail.originalMailId = "";
        mail.isTrash = false;
        await mailCollection.InsertOneAsync(mail);

        mail.isImportant = false;
        mail.labels = new List<string>();
        mail.category = string.Empty;
        var members = mail.to.Concat(mail.cc).Concat(mail.bcc).GroupBy(m => m.id).Select(g => g.First()).ToList();
        var recipientMails = new List<MailModel>();
        foreach (var recipient in mail.to)
        {
            var recipientMail = CreateRecipientMail(mail, recipient, members);
            recipientMails.Add(recipientMail);
        }
        foreach (var recipient in mail.cc)
        {
            var recipientMail = CreateRecipientMail(mail, recipient, members);
            recipientMails.Add(recipientMail);
        }
        foreach (var recipient in mail.bcc)
        {
            var recipientMail = CreateRecipientMail(mail, recipient, members);
            recipientMails.Add(recipientMail);
        }

        foreach (var recipientMail in recipientMails)
        {
            var folder = recipientMail.folder ?? "inbox";
            await mailCollection.InsertOneAsync(recipientMail);
        }
    }

    private static MailModel CreateRecipientMail(MailModel mail, MemberModel recipient, List<MemberModel> members)
    {
        var recipientMail = new MailModel{
            attachments = mail.attachments,
            author = mail.author,
            body = mail.body,
            from = mail.from,
            isDeleted = mail.isDeleted,
            isDraft = mail.isDraft,
            isPoll = mail.isPoll,
            originalMailId = mail.id,
            isRead = mail.isRead,
            pollId = mail.pollId,
            shortBody = mail.shortBody,
            signature= mail.signature,
            replies = mail.replies,
            isTrash = mail.isTrash,
            sentDate = mail.sentDate,
            isReply = mail.isReply,
            subject = mail.subject,
            folder  = "inbox",
            prevFolder = "inbox",
        };
        recipientMail.id = GenerateIDHelper.GenerateID("19012001");
        recipientMail.to = new List<MemberModel> { recipient };
        recipientMail.cc = new List<MemberModel>();
        recipientMail.bcc = new List<MemberModel>();
        
        var member = members.FirstOrDefault(x => x.id == recipient.id);
        if (member != null)
        {
            recipientMail.to[0].name = member.name;
            recipientMail.to[0].email = member.email;
        }
        return recipientMail;
    }

    // Đánh dấu đã đọc mail
    public static async Task Read(string id, bool isRead) 
    {
        var filter = Builders<MailModel>.Filter.Eq(x => x.id, id);
        var update = Builders<MailModel>.Update
            .Set(m => m.isRead, isRead);
        await mailCollection.UpdateOneAsync(filter, update);
        return;
    }


    // Gắn sao cho mail
    public static async Task Important(string id, bool isImportant)
    {
        var filter = Builders<MailModel>.Filter.Eq(x => x.id, id);
        var update = Builders<MailModel>.Update
            .Set(m => m.isImportant, isImportant);
        await mailCollection.UpdateOneAsync(filter, update);
        return;
    }

    // Xóa mail
    public static async Task Trash(string id, string folder)
    {
        var filter = Builders<MailModel>.Filter.Eq(x => x.id, id);
        var update = Builders<MailModel>.Update
            .Set(m => m.folder, "trash")
            .Set(m => m.isTrash, true);
        await mailCollection.UpdateOneAsync(filter, update);
        return;
    }
    // read + unread nhiều mail
    public static async Task ReadMails(List<string> mailIds, bool isReads) 
    {
        if(isReads)
        {
            var filter = Builders<MailModel>.Filter.In("id", mailIds);
            var update = Builders<MailModel>.Update.Set("isRead", true);
            await mailCollection.UpdateManyAsync(filter, update);
            return;
        }
        else
        {
            var filter = Builders<MailModel>.Filter.In("id", mailIds);
            var update = Builders<MailModel>.Update.Set("isRead", false);
            await mailCollection.UpdateManyAsync(filter, update);
            return;
        }
    }

    public static async Task ImportantMails(List<string> mailIds, bool isImportants)
    {
        if (isImportants)
        {
            var filter = Builders<MailModel>.Filter.In("id", mailIds);
            var update = Builders<MailModel>.Update.Set("isImportant", true);
            await mailCollection.UpdateManyAsync(filter, update);
            return;
        }
        else
        {
            var filter = Builders<MailModel>.Filter.In("id", mailIds);
            var update = Builders<MailModel>.Update.Set("isImportant", false);
            await mailCollection.UpdateManyAsync(filter, update);
            return;
        }
    }

    public static async Task TrashMails(List<string> mailIds)
    {
        var filter = Builders<MailModel>.Filter.In("id", mailIds);
        var update = Builders<MailModel>.Update
            .Set("folder", "trash")
            .Set("isTrash", true);
        await mailCollection.UpdateManyAsync(filter, update);
        return;
    } 

    public static async Task RestoreMails(List<string> mailIds)
    {
        var mails = await mailCollection.Find(m => mailIds.Contains(m.id)).ToListAsync();
        foreach (var mail in mails)
        {
            mail.folder = mail.prevFolder;
            mail.isTrash = false;
            await mailCollection.ReplaceOneAsync(m => m.id == mail.id, mail);
        }
        return;
    }


    public static async Task UpdateLabels(List<string> mailIds, List<string> labelIds)
    {
        var filter = Builders<MailModel>.Filter.In(x => x.id, mailIds);
        var update = Builders<MailModel>.Update.Set(x => x.labels, labelIds);
        await mailCollection.UpdateManyAsync(filter, update);
        return;
    }

    public static async Task Draft(MailModel mail)
    {
        var id = GenerateIDHelper.GenerateID("19012001");
        mail.id = id;
        mail.isDeleted = false;
        mail.isRead = false;
        mail.isTrash = false;
        await mailCollection.InsertOneAsync(mail);
        return;
    }

}