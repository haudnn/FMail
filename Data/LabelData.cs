using Workdo.Helpers;
using Workdo.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;


public class LabelData {
    public static IMongoCollection<LabelModel> labelCollection = ConnectDB<LabelModel>.GetClient("mailbox", "label");


    public static async Task Create(LabelModel label)
    {
        await labelCollection.InsertOneAsync(label);
        return;
    }
    

    public static async Task<List<LabelModel>> GetList(string author) 
    {
        return await labelCollection.Find(x => x.author == author).ToListAsync();
    }


    public static async Task Delete(LabelModel label) 
    {
        var filter = Builders<LabelModel>.Filter.Eq(x => x.id, label.id);
        await labelCollection.DeleteOneAsync(filter);
        return;
    }

    public static async Task Update(LabelModel label)
    {
        var filter = Builders<LabelModel>.Filter.Eq(x => x.id, label.id);
        var update = Builders<LabelModel>.Update
            .Set(l => l.name, label.name)
            .Set(l => l.color, label.color);
        await labelCollection.ReplaceOneAsync(filter, label);
        return;
    }


    public static async Task<List<LabelModel>> GetLabelsById(List<string> ids)
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