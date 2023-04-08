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
            return false;
        }
    }
    

    public static async Task<PollModel> GetPollById(string pollId) 
    {
        return await pollCollection.Find(x => x.id == pollId).FirstOrDefaultAsync();
    }


    public static async Task Vote(string pollId, string questionId, string choiceId, MemberModel voter)
    {
        try
        {
            var filter = Builders<PollModel>.Filter.And(
                Builders<PollModel>.Filter.Eq("_id", pollId),
                Builders<PollModel>.Filter.Eq("questions._id", questionId),
                Builders<PollModel>.Filter.Eq("questions.choices._id", choiceId)
            );
            var update = Builders<PollModel>.Update.Push("questions.$.choices.$[choice].voters", voter);
            var arrayFilters = new List<ArrayFilterDefinition> 
            {
                new BsonDocumentArrayFilterDefinition<BsonDocument>(
                new BsonDocument("choice._id", choiceId)
                )
            };
            var result = await pollCollection.UpdateOneAsync(filter, update, new UpdateOptions
            {
                ArrayFilters = arrayFilters
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }


    public static async Task<PollModel> UnVote(string pollId, string questionId, string choiceId, MemberModel voter)
    {
        try
        {
            var filter = Builders<PollModel>.Filter.And(
                Builders<PollModel>.Filter.Eq("_id", pollId),
                Builders<PollModel>.Filter.Eq("questions._id", questionId),
                Builders<PollModel>.Filter.Eq("questions.choices._id", choiceId)
            );
            var update = Builders<PollModel>.Update.PullFilter("questions.$.choices.$[choice].voters",
                Builders<MemberModel>.Filter.Eq("id", voter.id));
            var arrayFilters = new List<ArrayFilterDefinition>
        {
            new BsonDocumentArrayFilterDefinition<BsonDocument>(
                new BsonDocument("choice._id", choiceId)
            )
        };
            var options = new FindOneAndUpdateOptions<PollModel>
            {
                ReturnDocument = ReturnDocument.After,
                ArrayFilters = arrayFilters
            };
            var updatedPoll = await pollCollection.FindOneAndUpdateAsync(filter, update, options);
            return updatedPoll;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }

}