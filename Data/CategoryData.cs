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
    public static IMongoCollection<CategorySortedModel> sortedCollection = ConnectDB<CategorySortedModel>.GetClient("mailbox", "category_sorted");


    /// <summary>
    /// Tạo danh mục
    /// </summary>
    public static async Task Create(CategoryModel category)
    {
        var maxPosition = await categoryCollection.Find(x => true)
            .SortByDescending(x => x.position)
            .ToListAsync()
            .ContinueWith(t => t.Result.Count > 0 ? t.Result[0].position : 0);

        category.id = SharedHelperV2.GenerateID("19012001");
        category.position = maxPosition + 1;
        category.created_at = DateTime.Now.Ticks;
        await categoryCollection.InsertOneAsync(category);

        var filter = Builders<CategorySortedModel>.Filter.Empty;
        var update = Builders<CategorySortedModel>.Update.Push(x => x.sorted, category.position);
        await sortedCollection.UpdateManyAsync(filter, update);
    
        return;
    }



    /// <summary>
    /// Lây tất cả danh mục
    /// </summary>
    public static async Task<List<CategoryModel>> GetList() 
    {
        return await categoryCollection.Find(_ => true).ToListAsync();
    }

    /// <summary>
    /// Xóa danh mục
    /// </summary>

    public static async Task Delete(CategoryModel category) 
    {
        var filter = Builders<CategoryModel>.Filter.Eq(x => x.id, category.id);
        await categoryCollection.DeleteOneAsync(filter);


        var filterSorted = Builders<CategorySortedModel>.Filter.Empty;
        var update = Builders<CategorySortedModel>.Update.Pull(x => x.sorted, category.position);
        await sortedCollection.UpdateManyAsync(filterSorted, update);
        
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


    public static async Task Sorted(CategorySortedModel newSortedModel)
    {
        var isSorted = await sortedCollection.Find(x => x.author == newSortedModel.author).FirstOrDefaultAsync();
        if (isSorted == null)
        {
            newSortedModel.id = SharedHelperV2.GenerateID("19012001");
            await sortedCollection.InsertOneAsync(newSortedModel);
            return;
        }
        var filter = Builders<CategorySortedModel>.Filter.Eq(x => x.author, newSortedModel.author);
        await sortedCollection.ReplaceOneAsync(filter, newSortedModel);
    }


    public static async Task<CategorySortedModel> GetSorted(string author)
    {
        var isSorted =  await sortedCollection.Find(x => x.author == author).FirstOrDefaultAsync();

        if( isSorted ==  null )
        {
            return new CategorySortedModel();
        }
        return isSorted;
    }


}