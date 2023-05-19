using Workdo.Helpers;
using MongoDB.Driver;
using Workdo.Models;
using MongoDB.Bson;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Workdo.Data;



public class PollData
{

    public static IMongoCollection<PollModel> pollCollection = ConnectDB<PollModel>.GetClient("mailbox", "poll");


    public static async Task<bool> Create(PollModel pollToCreate)
    {
       try 
        {
            await pollCollection.InsertOneAsync(pollToCreate);
            return true;
        }
       catch(Exception ex)
        {
            return false;
        }
    }
    

    public static async Task<PollModel> Get(string pollId) 
    {
        return await pollCollection.Find(x => x.id == pollId).FirstOrDefaultAsync();
    }


    public static async Task Vote(PollModel poll)
    {
        var filter = Builders<PollModel>.Filter.Eq(p => p.id, poll.id);
        await pollCollection.ReplaceOneAsync(filter, poll);
    }




    public static async Task Stop(string id)
    {
        var filter = Builders<PollModel>.Filter.Eq(x => x.id, id);
        var update = Builders<PollModel>.Update.Set("isStopped", true);
        await pollCollection.UpdateOneAsync(filter, update);
    }

}