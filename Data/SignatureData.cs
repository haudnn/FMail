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
    private static IMongoClient _client = ConnectDB.GetClient();
    private static IMongoDatabase database = _client.GetDatabase("mailbox");
    private static IMongoCollection<SignatureModel> signatureCollection = database.GetCollection<SignatureModel>("signature");



    public static async Task<bool> CreateSignature(SignatureModel signature) 
    {
        var isExistSignature = await signatureCollection.Find(x => x.name == signature.name).FirstOrDefaultAsync();
        if (isExistSignature == null)
        {
            signatureCollection.InsertOne(signature);
            return true;
        }
        return false;
    }
    

    public static async Task<List<SignatureModel>> GetAllSignatures() 
    { 
        List<SignatureModel> signatures = new List<SignatureModel>();
        // var sort = Builders<BsonDocument>.Sort.Ascending("createdAt");
        signatures = await signatureCollection.Find(_ => true).ToListAsync();
        return signatures;
    }

    
    public static async Task<SignatureModel> GetSignatureById(string id)
    {
        SignatureModel signature = new SignatureModel();
        var isFoundSignature = await signatureCollection.Find(x => x.id == id).FirstOrDefaultAsync();
        if( isFoundSignature != null ){
            signature = isFoundSignature;
        }
        return signature;
    }
    

    public static async Task DeleteSignature(string id)
    {
        var filter = Builders<SignatureModel>.Filter.Eq(signature => signature.id, id);
        await signatureCollection.DeleteOneAsync(filter);
        return;
    }


    public static async Task SetDefaultSignature(string signatureId, string userid)
    {
        var isFoundSignature = await signatureCollection.Find(x => x.id == signatureId).FirstOrDefaultAsync();
        if(isFoundSignature != null) { 
            // find in user model => set default signature = id 
        }
        return;
    }


    public static async Task UpdateSignature(SignatureModel signature) 
    {

        var filter = Builders<SignatureModel>.Filter.Eq(x => x.id, signature.id);
        var update = Builders<SignatureModel>.Update
            .Set(s => s.name, signature.name)
            .Set(s => s.body, signature.body);
        await signatureCollection.ReplaceOneAsync(filter, signature);
        return;
    }
}