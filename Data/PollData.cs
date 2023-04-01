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
    private readonly IMongoClient _client;
    private readonly IMongoDatabase database;
    private readonly IMongoCollection<PollModel> pollCollection;
    public PollData()
    {
        var connectDB = new ConnectDB();
        _client = connectDB.GetClient();
        database = _client.GetDatabase("mailbox");
        pollCollection = database.GetCollection<PollModel>("poll");
    }

    public async Task<bool> CreatePoll(PollModel pollToCreate)
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
    


    public async Task<PollModel> GetPollById(string pollId) 
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