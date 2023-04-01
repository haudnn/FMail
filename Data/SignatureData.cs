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
    private readonly IMongoClient _client;
    private readonly IMongoDatabase database;
    private readonly IMongoCollection<SignatureModel> signatureCollection;
    public SignatureData()
    {
        var connectDB = new ConnectDB();
        _client = connectDB.GetClient();
        database = _client.GetDatabase("mailbox");
        signatureCollection = database.GetCollection<SignatureModel>("signature");
    }


    public async Task<bool> CreateSignature(SignatureModel signature) 
    {
        var isExistSignature = await signatureCollection.Find(x => x.name == signature.name).FirstOrDefaultAsync();
        if (isExistSignature == null)
        {
            signatureCollection.InsertOne(signature);
            return true;
        }
        return false;
    }
    

    public async Task<List<SignatureModel>> GetAllSignatures() 
    { 
        List<SignatureModel> signatures = new List<SignatureModel>();
        // var sort = Builders<BsonDocument>.Sort.Ascending("createdAt");
        signatures = await signatureCollection.Find(_ => true).ToListAsync();
        return signatures;
    }

    
    public async Task<SignatureModel> GetSignatureById(string id)
    {
        SignatureModel signature = new SignatureModel();
        var isFoundSignature = await signatureCollection.Find(x => x.id == id).FirstOrDefaultAsync();
        if( isFoundSignature != null ){
            signature = isFoundSignature;
        }
        return signature;
    }
    

    public async Task DeleteSignature(string id)
    {
        var filter = Builders<SignatureModel>.Filter.Eq(signature => signature.id, id);
        await signatureCollection.DeleteOneAsync(filter);
        return;
    }


    public async Task SetDefaultSignature(string signatureId, string userid)
    {
        var isFoundSignature = await signatureCollection.Find(x => x.id == signatureId).FirstOrDefaultAsync();
        if(isFoundSignature != null) { 
            // find in user model => set default signature = id 
        }
        return;
    }


    public async Task UpdateSignature(SignatureModel signature) 
    {

        var filter = Builders<SignatureModel>.Filter.Eq(x => x.id, signature.id);
        var update = Builders<SignatureModel>.Update
            .Set(s => s.name, signature.name)
            .Set(s => s.body, signature.body);
        await signatureCollection.ReplaceOneAsync(filter, signature);
        return;
    }
}