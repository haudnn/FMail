using Workdo.Helpers;
using Workdo.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;


public class GroupData {

    private readonly IMongoClient _client;
    private readonly IMongoDatabase database;
    private readonly IMongoCollection<GroupModel> groupCollection;


    public GroupData()
    {
        var connectDB = new ConnectDB();
        _client = connectDB.GetClient();
        database = _client.GetDatabase("mailbox");
        groupCollection = database.GetCollection<GroupModel>("group");
    }


    public async Task<bool> CreateGroup(GroupModel group)
    {
        var isExistGroup = await groupCollection.Find(x => x.name == group.name).FirstOrDefaultAsync();
        if( isExistGroup == null ) 
        {
            await groupCollection.InsertOneAsync(group);
            return true;
        }
        return false;
    }

    public async Task<List<GroupModel>> GetAllGroups() 
    { 
        List<GroupModel> groups = new List<GroupModel>();
        groups = await groupCollection.Find(_ => true).ToListAsync();
        return groups;
    } 



    public async Task<bool> UpdateGroup(string groupName, string groupId)
    {
        var isExistGroup = await groupCollection.Find(x => x.name ==groupName).FirstOrDefaultAsync();
        if (isExistGroup == null)
        {
            var filter = Builders<GroupModel>.Filter.Eq(x => x.id, groupId);
            var update = Builders<GroupModel>.Update
                .Set(g => g.name, groupName);
            await groupCollection.UpdateOneAsync(filter, update);
            return true;
        }
        return false;
    }


    public async Task DeleteMember(string groupId, string memberId) 
    { 
        var filter = Builders<GroupModel>.Filter.Eq(x => x.id, groupId);
        var update = Builders<GroupModel>.Update.Pull(x => x.members, memberId);
        await groupCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task<GroupModelDetail> GetGroupById(string groupId)
    {
        var isFoundGroup = await groupCollection.Find(x => x.id == groupId).FirstOrDefaultAsync();
        List<MemberModel> members = InitDataFakeHelper.GetMembersById(isFoundGroup.members);
        GroupModelDetail groupUpdating = new GroupModelDetail{
            id = isFoundGroup.id,
            name = isFoundGroup.name,
            members = members
        };
        return groupUpdating;
    }


    public async Task DeleteGroup(string groupId) 
    {
        var filter = Builders<GroupModel>.Filter.Eq(group => group.id, groupId);
        await groupCollection.DeleteOneAsync(filter);
        return;
    }

    public async Task AddMembers(string groupId, List<string> memberIds)
    {
        var filter = Builders<GroupModel>.Filter.Eq(x => x.id, groupId);
        var update = Builders<GroupModel>.Update.PushEach(x => x.members, memberIds, position: 0);
        await groupCollection.UpdateOneAsync(filter, update);
        return;
    }

}