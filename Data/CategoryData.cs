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


    /// <summary>
    /// Tạo danh mục
    /// </summary>
    public static async Task Create(CategoryModel category)
    {
        var maxPosition = await categoryCollection.Find(x => true)
            .SortByDescending(x => x.position)
            .ToListAsync()
            .ContinueWith(t => t.Result.Count > 0 ? t.Result[0].position : 0);

        category.id = GenerateIDHelper.GenerateID("19012001");
        category.position = maxPosition + 1;
        category.created_at = DateTime.Now.Ticks;
        await categoryCollection.InsertOneAsync(category);
        return;
    }



    /// <summary>
    /// Lây tất cả danh mục
    /// </summary>
    public static async Task<List<CategoryModel>> GetList() 
    {
        var sorted = Builders<CategoryModel>.Sort.Descending("created_at");
    
        return await categoryCollection.Find(_ => true).Sort(sorted).ToListAsync();
    }

    /// <summary>
    /// Xóa danh mục
    /// </summary>

    public static async Task Delete(CategoryModel category) 
    {
        var filter = Builders<CategoryModel>.Filter.Eq(x => x.id, category.id);
        await categoryCollection.DeleteOneAsync(filter);
        return;
    }

    /// <summary>
    /// Cập nhật danh mục
    /// </summary>
    public static async Task Update(CategoryModel category) 
    {
        var filter = Builders<CategoryModel>.Filter.Eq(x => x.id, category.id);
        var update = Builders<CategoryModel>.Update
            .Set(c => c.name, category.name);
        await categoryCollection.ReplaceOneAsync(filter, category);
        return;
    }

    /// <summary>
    /// Lây danh mục từ id
    /// </summary>

    public static async Task<CategoryModel> Get(string id)
    {
        return await categoryCollection.Find(c => c.id == id).FirstOrDefaultAsync();
    }


}