using Workdo.Helpers;
using Workdo.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

public class CategoryData
{
    private readonly IMongoClient _client;
    private readonly IMongoDatabase database;
    private readonly IMongoCollection<CategoryModel> categoryCollection;

    public CategoryData()
    {
        var connectDB = new ConnectDB();
        _client = connectDB.GetClient();
        database = _client.GetDatabase("mailbox");
        categoryCollection = database.GetCollection<CategoryModel>("category");
    }


    public async Task CreateCategory(CategoryModel category)
    {
        var maxPosition = await categoryCollection.Find(x => true)
            .SortByDescending(x => x.position)
            .ToListAsync()
            .ContinueWith(t => t.Result.Count > 0 ? t.Result[0].position : 0);
        CategoryModel categoryToCreate = new CategoryModel
        {
            id = GenerateIDHelper.GenerateID("19012001"),
            name = category.name,
            position = maxPosition + 1,
            createdAt = DateTime.UtcNow

    };
        await categoryCollection.InsertOneAsync(categoryToCreate);
        return;
    }


    public async Task<List<CategoryModel>> GetAllCategory() 
    {
        List<CategoryModel> categories = new List<CategoryModel>();
        categories = await categoryCollection.Find(_ => true).ToListAsync();
        return categories;
    }


 
    public async Task DeleteCategory(CategoryModel category) 
    {
        var filter = Builders<CategoryModel>.Filter.Eq(x => x.id, category.id);
        await categoryCollection.DeleteOneAsync(filter);
        return;
    }

    public async Task UpdateCategory(CategoryModel category) 
    {
        var filter = Builders<CategoryModel>.Filter.Eq(x => x.id, category.id);
        var update = Builders<CategoryModel>.Update
            .Set(c => c.name, category.name);
        await categoryCollection.ReplaceOneAsync(filter, category);
        return;
    }

 }