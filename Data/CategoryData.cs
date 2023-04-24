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

    public static IMongoCollection<CategoryModel> categoryCollection = ConnectDB<CategoryModel>.GetClient("mailbox", "category");

    public static async Task CreateCategory(CategoryModel category)
    {
        var maxPosition = await categoryCollection.Find(x => true)
            .SortByDescending(x => x.position)
            .ToListAsync()
            .ContinueWith(t => t.Result.Count > 0 ? t.Result[0].position : 0);
        category.id = GenerateIDHelper.GenerateID("19012001");
        category.position = maxPosition + 1;
        category.createdAt = DateTime.UtcNow;
        await categoryCollection.InsertOneAsync(category);
        return;
    }


    public static async Task<List<CategoryModel>> GetAllCategory() 
    {
        return await categoryCollection.Find(_ => true).ToListAsync();
    }


 
    public static async Task DeleteCategory(CategoryModel category) 
    {
        var filter = Builders<CategoryModel>.Filter.Eq(x => x.id, category.id);
        await categoryCollection.DeleteOneAsync(filter);
        return;
    }

    public static async Task UpdateCategory(CategoryModel category) 
    {
        var filter = Builders<CategoryModel>.Filter.Eq(x => x.id, category.id);
        var update = Builders<CategoryModel>.Update
            .Set(c => c.name, category.name);
        await categoryCollection.ReplaceOneAsync(filter, category);
        return;
    }

    public static async Task<CategoryModel> GetCategoryById(string id)
    {
        return await categoryCollection.Find(c => c.id == id).FirstOrDefaultAsync();
    }


}