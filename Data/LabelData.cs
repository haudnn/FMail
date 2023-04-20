using Workdo.Helpers;
using Workdo.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;


public class LabelData {
    private static IMongoClient _client = ConnectDB.GetClient();
    private static IMongoDatabase database = _client.GetDatabase("mailbox");
    private static IMongoCollection<LabelModel> labelCollection = database.GetCollection<LabelModel>("label");


    public static async Task CreateLabel(LabelModel label)
    {
        await labelCollection.InsertOneAsync(label);
        return;
    }
    

    public static async Task<List<LabelModel>> GetAllLabels(string author) 
    {
        List<LabelModel> labels = new List<LabelModel>();
        labels = await labelCollection.Find(x => x.author == author).ToListAsync();
        return labels;
    }


    public static async Task DeleteLabel(LabelModel label) 
    {
        var filter = Builders<LabelModel>.Filter.Eq(x => x.id, label.id);
        await labelCollection.DeleteOneAsync(filter);
        return;
    }

    public static async Task UpdateLabel(LabelModel label)
    {
        var filter = Builders<LabelModel>.Filter.Eq(x => x.id, label.id);
        var update = Builders<LabelModel>.Update
            .Set(l => l.name, label.name)
            .Set(l => l.color, label.color);
        await labelCollection.ReplaceOneAsync(filter, label);
        return;
    }


    public static async Task<List<LabelModel>> FindLabelsByIds(List<string> ids)
    {
        if (ids == null || ids.Count == 0)
        {
            return new List<LabelModel>();
        }

        var filter = Builders<LabelModel>.Filter.In("id", ids);
        var labels = await labelCollection.Find(filter).ToListAsync();
        return labels;

    }
}