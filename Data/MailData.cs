using Workdo.Helpers;
using MongoDB.Driver;
using Workdo.Models;
using MongoDB.Bson;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Workdo.Data;



public class MailData 
{

    private static IMongoClient _client = ConnectDB.GetClient();
    private static IMongoDatabase database = _client.GetDatabase("mailbox");
    private static IMongoCollection<MailModel> mailCollection = database.GetCollection<MailModel>("mail");
    private static IMongoCollection<LabelModel> labelCollection = database.GetCollection<LabelModel>("label");




    public static async Task CreateMail(MailModel mailToCreate)
    {
        await mailCollection.InsertOneAsync(mailToCreate);
        mailToCreate.folder = "inbox";
        mailToCreate.isImportant = false;
        mailToCreate.labels = new List<string>();
        mailToCreate.category = string.Empty;
        foreach (var recipient in mailToCreate.to)
        {
            mailToCreate.id = GenerateIDHelper.GenerateID("19012001");
            mailToCreate.to = new List<MemberModel> { recipient };
            await mailCollection.InsertOneAsync(mailToCreate);
        }
        return;
    }


    public static async Task<List<MailModel>> GetAllMailsByFolder(string folderName, string userid, bool isSent)
    {
        List<MailModel> mails = new List<MailModel>();
        if(!isSent) 
        {
            var filter = Builders<MailModel>.Filter.And(
                Builders<MailModel>.Filter.Eq(x => x.isDraft, false),
                Builders<MailModel>.Filter.Eq(x => x.isDeleted, false),
                Builders<MailModel>.Filter.Eq(x => x.folder, folderName),
                Builders<MailModel>.Filter.Where(x => x.to.Any(t => t.id == userid))
            );
            mails = await mailCollection.Find(filter).ToListAsync();
            return mails;
        }
        else
        {
            var filter = Builders<MailModel>.Filter.And(
                Builders<MailModel>.Filter.Eq(x => x.isDraft, false),
                Builders<MailModel>.Filter.Eq(x => x.isDeleted, false),
                Builders<MailModel>.Filter.Eq(x => x.folder, folderName),
                Builders<MailModel>.Filter.Eq(x => x.author, userid)
            );
            mails = await mailCollection.Find(filter).ToListAsync();
            return mails;
        }
    }



    public static async Task<MailModel> GetMailById(string id) 
    {
        MailModel mail = new MailModel();
        var isFoundMail = await mailCollection.Find(x => x.id == id).FirstOrDefaultAsync();
        if (isFoundMail != null)
        {
           mail = isFoundMail;
        }
        return mail;
    }


    public static async Task ReadMail(string id, bool isRead) 
    {
        var filter = Builders<MailModel>.Filter.Eq(x => x.id, id);
        var update = Builders<MailModel>.Update
            .Set(m => m.isRead, isRead);
        await mailCollection.UpdateOneAsync(filter, update);
        return;
    }

    public static async Task Vote(){}
}