
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

    public static async Task<List<MailModel>> GetList(string folderName, string userid, string category = "all")
    {
        List<MailModel> mails = new List<MailModel>();
        if(folderName == "trash")
        {
            return await GetMailsTrash(userid);
        }

        if(folderName == "sent")
        {
            return await GetMailsSent(category, userid);
        }

        if(folderName == "important")
        {
            return await GetMailsImportant(category, userid);
        }

        if(folderName == "inbox")
        {
            return await GetMailsInbox(category, userid);
        }

        if (folderName == "draft")
        {
            return await GetMailsDraft(category, userid);
        }
        return mails;
    }


    public static async Task<List<MailModel>> GetMailsDraft(string category, string userid)
    {

        var builder = Builders<MailModel>.Filter;

        var filtered = 
            builder.Eq("isDraft", true) 
            & builder.Eq("isTrash", false)
            & builder.Eq("author", userid);

        if (category != "all")
        {
            filtered = filtered & builder.Eq("category", category);
        }

        return await mailCollection.Find(filtered).ToListAsync();
    }



    public static async Task<List<MailModel>> GetMailsTrash(string userid)
    {

        var builder = Builders<MailModel>.Filter;

        var filtered =
            builder.Eq("isDeleted", false)
            & builder.Eq("isTrash", true)
            & builder.Eq("author", userid);

        return await mailCollection.Find(filtered).ToListAsync();
    }




    public static async Task<List<MailModel>> GetMailsInbox(string category, string userId)
    {
        var builder = Builders<MailModel>.Filter;
        var filtered = builder.Eq("author", userId) & builder.Eq("isTrash", false) & builder.Eq("isReply", false);
        if (category != "all")
        {
            filtered = filtered & builder.Eq("category", category);
        }
        var mails = await mailCollection.Find(filtered).ToListAsync();

        var mailsFiltered = new List<MailModel>();
        foreach (var mail in mails)
        {
            if ((mail.from == userId) && ((await RepliesCount(mail.id, userId) > 0)))
            {
                mailsFiltered.Add(mail);
            }
            if (mail.to.Contains(userId) || mail.cc.Contains(userId) || mail.bcc.Contains(userId))
            {
                mailsFiltered.Add(mail);
            }
        }
        return mailsFiltered;
    }


    public static async Task<List<MailModel>> GetMailsSent(string category, string userid)
    {
        var mailsFiltered = new List<MailModel>();
        var builder = Builders<MailModel>.Filter;
        var filtered = builder.Eq("author", userid) & builder.Eq("isTrash" , false) & builder.Eq("isReply", false);
        if (category != "all")
        {
            filtered = filtered & builder.Eq("category", category);
        }
        var mails = await mailCollection.Find(filtered).ToListAsync();
        foreach (var mail in mails)
        {
            if ((mail.from == mail.author) && (mail.originalMailId == ""))
            {
                mailsFiltered.Add(mail);
            }
            if ((mail.to.Contains(userid) || mail.cc.Contains(userid) || mail.bcc.Contains(userid)) && (mail.originalMailId != ""))
            {
                if(await RepliesCount(mail.id, userid) > 0)
                {
                    mailsFiltered.Add(mail);
                }

            }
        }
        return mailsFiltered;
    }




    public static async Task<List<MailModel>> GetMailsImportant(string category, string userid)
    {
        var builder = Builders<MailModel>.Filter;
        var filtered =
            builder.Eq("author", userid) &
            builder.Eq("isTrash", false) &
            builder.Eq("isImportant", true);

        if (category != "all")
        {
            filtered = filtered & builder.Eq("category", category);
        }

        return await mailCollection.Find(filtered).ToListAsync();
    }

    public static async Task<List<MailModel>> GetMailsByLabel(string labelId, string userid)
    {
        var builder = Builders<MailModel>.Filter;

        var filtered =
            builder.Where(x => x.labels.Any(t => t == labelId)) &
            builder.Eq("isTrash", false) &
            builder.Eq("author", userid);

        return await mailCollection.Find(filtered).ToListAsync();
    }


    public static async Task<MailModel> GetMailById(string id)
    {
        return await mailCollection.Find(x => x.id == id).FirstOrDefaultAsync();
    }


    public static async Task CreateMail(MailModel mail, bool isDraft = false, string mailDraftId = "")
    {
        if(isDraft && !string.IsNullOrEmpty(mailDraftId))
        {
            var filter = Builders<MailModel>.Filter.Eq(x => x.id, mailDraftId);
            mail.id = mailDraftId;
            await mailCollection.ReplaceOneAsync(filter, mail);
        }
        else
        {
            var id = GenerateIDHelper.GenerateID("19012001");
            mail.id = id;
            await mailCollection.InsertOneAsync(mail);
        }

        var members = mail.to.Concat(mail.cc).Concat(mail.bcc).GroupBy(m => m).Select(g => g.First()).ToList();
        var recipientMails = new List<MailModel>();
        foreach (var recipient in members)
        {
            var recipientMail = CreateRecipientMail(mail, recipient, members, mailDraftId);
            recipientMails.Add(recipientMail);
        }
        foreach (var recipientMail in recipientMails)
        {
            await mailCollection.InsertOneAsync(recipientMail);
        }
    }

    private static MailModel CreateRecipientMail(MailModel mail, string recipient, List<string> members , string mailDraftId = "")
    {
        var originalMailId = !String.IsNullOrEmpty(mailDraftId) ? mailDraftId : mail.id;
        var recipientMail = new MailModel
        {
            attachments = mail.attachments,
            body = mail.body,
            from = mail.from,
            isDeleted = false,
            isDraft = false,
            originalMailId = originalMailId,
            isRead = false,
            category = mail.category,
            pollId = mail.pollId,
            shortBody = mail.shortBody,
            signature = mail.signature,
            isTrash = false,
            sentDate = mail.sentDate,
            isReply = false,
            subject = mail.subject,
            to = mail.to,
            isImportant = false,
            parentId = String.Empty,
            labels = new List<string>(),
            cc = mail.cc,
            bcc = mail.bcc,
            created_at = DateTime.Now.Ticks,
        };
        recipientMail.id = GenerateIDHelper.GenerateID("19012001");
        recipientMail.author = recipient;
        return recipientMail;
    }


    public static async Task Reply(MailModel mail, string idMailToReply, string mailIdClicked, bool isReplyMail = true)
    {  

        // forward the mail
        if(!isReplyMail)
        {

        }

        var currentMailReply = await mailCollection.Find(x => x.id == mailIdClicked).FirstOrDefaultAsync();
        string customHtml = "";
        DateTime datetime = DateTime.Parse(currentMailReply.sentDate);
        string output = datetime.ToString("dd 'thg' M',' yyyy 'vào lúc' HH:mm");
        MemberModel member = InitDataFakeHelper.GetMemberById(currentMailReply.from);
        if(currentMailReply.parentId == null)
        {
            customHtml = "<div>" + "<div>" + "Vào" + " " + output + " " + member.name + " "  + $"{member.email}"  + " " +  "đã viết:" +  "<div style=\"margin:0px 0px 0px 0.8ex;border-left:1px solid rgb(204,204,204);padding-left:1ex\">" + currentMailReply.body + "</div>" + "</div>"  + "</div>";
        }
        else
        {

            customHtml = "<div>" + "<div>" + "Vào" + " " + output + " " + member.name + " " + $"{member.email}" + " " + "đã viết:" + "<div style=\"margin:0px 0px 0px 0.8ex;border-left:1px solid rgb(204,204,204);padding-left:1ex\">" + currentMailReply.body +  currentMailReply.parentId + "</div>" + "</div>" + "</div>";

        }
        var mailReply = await mailCollection.Find(x => x.id == idMailToReply).FirstOrDefaultAsync();
        mail.id = GenerateIDHelper.GenerateID("19012001"); ;
        mail.category = mailReply.category;
        mail.parentId = customHtml;
        mail.originalMailId = mailReply.originalMailId != "" ? mailReply.originalMailId : idMailToReply;

        await mailCollection.InsertOneAsync(mail);

        var recipients = mail.to;
        foreach (var recipient in recipients)
        {
            mail.id = GenerateIDHelper.GenerateID("19012001");
            mail.originalMailId = mailReply.originalMailId != "" ? mailReply.originalMailId : idMailToReply;
            mail.category = mailReply.category;
            mail.parentId = customHtml;
            mail.author = recipient;
            mail.to = mail.to;
            await mailCollection.InsertOneAsync(mail);
        }
    }

    public static async Task<List<MailModel>> GetMailThread(string originalMailId , string author)
    {
        var mails = await mailCollection.Find(x => 
            x.originalMailId == originalMailId && 
            x.author == author && 
            x.isReply == true).ToListAsync();

        return mails;
        
    }



    public static async Task Read(string id, bool isRead) 
    {
        var filter = Builders<MailModel>.Filter.Eq(x => x.id, id);
        var update = Builders<MailModel>.Update.Set("isRead", isRead);
        await mailCollection.UpdateOneAsync(filter, update);
        return;
    }





    // GẮN SAO
    public static async Task Important(string id, bool isImportant)
    {
        var filter = Builders<MailModel>.Filter.Eq(x => x.id, id);
        var update = Builders<MailModel>.Update.Set("isImportant", isImportant);
        await mailCollection.UpdateOneAsync(filter, update);
        return;
    }

    public static async Task Trash(string id)
    {
        var filter = Builders<MailModel>.Filter.Eq(x => x.id, id);
        var update = Builders<MailModel>.Update.Set("isTrash", true);
        await mailCollection.UpdateOneAsync(filter, update);
        return;
    }


    public static async Task ReadMails(List<string> mailIds, bool isReads) 
    {
        var filter = Builders<MailModel>.Filter.In("id", mailIds);
        var update = Builders<MailModel>.Update.Set("isRead", isReads);
        await mailCollection.UpdateManyAsync(filter, update);
        return;
    }

    public static async Task ImportantMails(List<string> mailIds, bool isImportants)
    {
        var filter = Builders<MailModel>.Filter.In("id", mailIds);
        var update = Builders<MailModel>.Update.Set("isImportant", isImportants);
        await mailCollection.UpdateManyAsync(filter, update);
        return;
    }

    public static async Task TrashMails(List<string> mailIds)
    {
        var filter = Builders<MailModel>.Filter.In("id", mailIds);
        var update = Builders<MailModel>.Update
            .Set("isTrash", true);
        await mailCollection.UpdateManyAsync(filter, update);
        return;
    } 

    public static async Task RestoreMails(List<string> mailIds)
    {
        var filter = Builders<MailModel>.Filter.In("id", mailIds);
        var update = Builders<MailModel>.Update
            .Set("isTrash", false);
        await mailCollection.UpdateManyAsync(filter, update);
        return;
    }
    
    public static async Task DeleteMails(List<string> mailIds)
    {
        var filter = Builders<MailModel>.Filter.In("id", mailIds);
        var update = Builders<MailModel>.Update
            .Set("isTrash", true)
            .Set("isDeleted", true);
        await mailCollection.UpdateManyAsync(filter, update);
        return;
    }


    public static async Task UpdateLabels(List<string> mailIds, List<string> labelIds)
    {
        var filter = Builders<MailModel>.Filter.In(x => x.id, mailIds);
        var update = Builders<MailModel>.Update.Set(x => x.labels, labelIds);
        await mailCollection.UpdateManyAsync(filter, update);
        return;
    }

    public static async Task RemoveLabels(List<string> mailIds, List<string> labelIds)
    {
        var filter = Builders<MailModel>.Filter.In(x => x.id, mailIds);
        var update = Builders<MailModel>.Update.PullAll(x => x.labels, labelIds);
        await mailCollection.UpdateManyAsync(filter, update);
        return;
    }

    public static async Task AddLabel(string mailId, string labelId)
    {
        var filter = Builders<MailModel>.Filter.Eq(x => x.id, mailId);
        var update = Builders<MailModel>.Update.AddToSet(x => x.labels, labelId);
        await mailCollection.UpdateOneAsync(filter, update);
        return;
    }

    public static async Task RemoveLabel(string mailId, string labelId)
    {
        var filter = Builders<MailModel>.Filter.Eq(x => x.id, mailId);
        var update = Builders<MailModel>.Update.Pull(x => x.labels, labelId);
        await mailCollection.UpdateOneAsync(filter, update);
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


    public static async Task<MailModel> GetReply(string originalMail, string author)
    {
        var findOriginalMail = await mailCollection.Find(x => x.id == originalMail).FirstOrDefaultAsync();
        if(string.IsNullOrEmpty(findOriginalMail.originalMailId))
        {
            return await mailCollection.Find(x => x.from == author && x.originalMailId == originalMail).SortByDescending(x => x.created_at).FirstOrDefaultAsync();
        }
        return await mailCollection.Find(x => x.from == author && x.originalMailId == findOriginalMail.originalMailId).SortByDescending(x => x.created_at).FirstOrDefaultAsync();
    }

    public static async Task<long> RepliesCount(string originalMail, string author)
    {
        
        var findOriginalMail = await mailCollection.Find(x => x.id == originalMail).FirstOrDefaultAsync();
        List<MailModel> mails = new List<MailModel>();
        if (string.IsNullOrEmpty(findOriginalMail.originalMailId))
        {
            mails = await mailCollection.Find(x => x.author == author &&  x.isReply == true && x.originalMailId == originalMail).ToListAsync();
        }
        else 
        {
            mails = await mailCollection.Find(x => x.author == author &&  x.isReply == true && x.originalMailId == findOriginalMail.originalMailId).ToListAsync();
        }
        return mails.Count;
    }
}