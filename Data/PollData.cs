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
    private static IMongoClient _client = ConnectDB.GetClient();
    private static IMongoDatabase database = _client.GetDatabase("mailbox");
    private static IMongoCollection<PollModel> pollCollection = database.GetCollection<PollModel>("poll");


    public static async Task<bool> CreatePoll(PollModel pollToCreate)
    {
       try 
        {
            await pollCollection.InsertOneAsync(pollToCreate);
            return true;
        }
       catch(Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
    


    public static async Task<PollModel> GetPollById(string pollId) 
    {
        PollModel poll = new PollModel();
        var isFoundPoll = await pollCollection.Find(x => x.id == pollId).FirstOrDefaultAsync();
        if (isFoundPoll != null)
        {
            poll = isFoundPoll;
        }
        return poll;
    }
}