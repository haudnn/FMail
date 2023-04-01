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
    private readonly IMongoClient _client;
    private readonly IMongoDatabase database;
    private readonly IMongoCollection<MailModel> mailCollection;
    private readonly IMongoCollection<LabelModel> labelCollection;

    public MailData()
    {
        var connectDB = new ConnectDB();
        _client = connectDB.GetClient();
        database = _client.GetDatabase("mailbox");
        mailCollection = database.GetCollection<MailModel>("mail");
        labelCollection = database.GetCollection<LabelModel>("label");
        
    }


    public async Task CreateMail(MailModel mailToCreate)
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


    public async Task<List<MailModel>> GetAllMailsByFolder(string folderName, string userid, bool isSent)
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



    public async Task<MailModel> GetMailById(string id) 
    {
        MailModel mail = new MailModel();
        var isFoundMail = await mailCollection.Find(x => x.id == id).FirstOrDefaultAsync();
        if (isFoundMail != null)
        {
           mail = isFoundMail;
        }
        return mail;
    }


    public async Task ReadMail(string id, bool isRead) 
    {
        var filter = Builders<MailModel>.Filter.Eq(x => x.id, id);
        var update = Builders<MailModel>.Update
            .Set(m => m.isRead, isRead);
        await mailCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task Vote(){}
}