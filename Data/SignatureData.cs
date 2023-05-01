using Workdo.Helpers;
using Workdo.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;


namespace Workdo.Data;


public class SignatureData
{

    public static IMongoCollection<SignatureModel> signatureCollection = ConnectDB<SignatureModel>.GetClient("mailbox", "signature");

    public static async Task<bool> Create(SignatureModel signature, string author) 
    {
        var isExistSignature = await signatureCollection.Find(x => x.name == signature.name && x.author == author).FirstOrDefaultAsync();

        if (isExistSignature == null)
        {
            signatureCollection.InsertOne(signature);
            return true;
        }

        return false;
    }
    

    public static async Task<List<SignatureModel>> GetList(string author) 
    { 
        return await signatureCollection.Find(x => x.author == author).ToListAsync();
    }


    public static async Task<SignatureModel> Get(string id)
    {
        var signature = await signatureCollection.Find(x => x.id == id).FirstOrDefaultAsync();
        return signature ?? new SignatureModel();
    }
    

    public static async Task Delete(string id)
    {
        var filter = Builders<SignatureModel>.Filter.Eq(signature => signature.id, id);
        await signatureCollection.DeleteOneAsync(filter);
        return;
    }


    public static async Task SetDefault(SignatureModel signature, string author)
    {
        var builder = Builders<SignatureModel>.Filter;
        var filter = builder.Eq("author", author) & builder.Ne("id", signature?.id);

        var update = Builders<SignatureModel>.Update.Set("isDefault", false);
        await signatureCollection.UpdateOneAsync(filter, update);

        if (signature != null)
        {
            filter = builder.Eq("author", author) & builder.Eq("id", signature.id);
            update = Builders<SignatureModel>.Update.Set("isDefault", true);
            await signatureCollection.UpdateOneAsync(filter, update);
        }
    }


    public static async Task Update(SignatureModel signature)
    {
        var filter = Builders<SignatureModel>.Filter.Eq(x => x.id, signature.id);
        var update = Builders<SignatureModel>.Update
            .Set(s => s.name, signature.name)
            .Set(s => s.body, signature.body);
        await signatureCollection.UpdateOneAsync(filter, update);
    }

    public static async Task<SignatureModel> GetDefault(string author)
    {
        var filter = Builders<SignatureModel>.Filter.Eq(x => x.author, author) & Builders<SignatureModel>.Filter.Eq(x => x.isDefault, true);
        var signature = await signatureCollection.Find(filter).FirstOrDefaultAsync();
        return signature ?? new SignatureModel();
    }
}