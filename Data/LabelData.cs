using Workdo.Helpers;
using Workdo.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;


public class LabelData {
    private readonly IMongoClient _client;
    private readonly IMongoDatabase database;
    private readonly IMongoCollection<LabelModel> labelCollection;

    public LabelData()
    {
        var connectDB = new ConnectDB();
        _client = connectDB.GetClient();
        database = _client.GetDatabase("mailbox");
        labelCollection = database.GetCollection<LabelModel>("label");
    }

    public async Task CreateLabel(LabelModel label)
    {
        LabelModel labelToCreate = new LabelModel
        {
            id = GenerateIDHelper.GenerateID("19012001"),
            name = label.name,
            color = label.color
        };
        await labelCollection.InsertOneAsync(labelToCreate);
        return;
    }
    

    public async Task<List<LabelModel>> GetAllLabels() 
    {
        List<LabelModel> labels = new List<LabelModel>();
        labels = await labelCollection.Find(_ => true).ToListAsync();
        return labels;
    }


    public async Task DeleteLabel(LabelModel label) 
    {
        var filter = Builders<LabelModel>.Filter.Eq(x => x.id, label.id);
        await labelCollection.DeleteOneAsync(filter);
        return;
    }

    public async Task UpdateLabel(LabelModel label)
    {
        var filter = Builders<LabelModel>.Filter.Eq(x => x.id, label.id);
        var update = Builders<LabelModel>.Update
            .Set(l => l.name, label.name)
            .Set(l => l.color, label.color);
        await labelCollection.ReplaceOneAsync(filter, label);
        return;
    }


    public async Task<List<LabelModel>> FindLabelsByIds(List<string> ids)
    {
        if (ids == null || ids.Count == 0)
        {
            return new List<LabelModel>();
        }

        var filter = Builders<LabelModel>.Filter.In(x => x.id, ids);
        var labels = await labelCollection.Find(filter).ToListAsync();
        return labels;

    }
}