using Workdo.Helpers;
using Workdo.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;


public class GroupData {

    private static IMongoClient _client = ConnectDB.GetClient();
    private static IMongoDatabase database = _client.GetDatabase("mailbox");
    private static IMongoCollection<GroupModel> groupCollection = database.GetCollection<GroupModel>("group");



    public static async Task<bool> CreateGroup(GroupModel group)
    {
        var isExistGroup = await groupCollection.Find(x => x.name == group.name).FirstOrDefaultAsync();
        if( isExistGroup == null ) 
        {
            await groupCollection.InsertOneAsync(group);
            return true;
        }
        return false;
    }

    public static async Task<List<GroupModel>> GetAllGroups() 
    { 
        List<GroupModel> groups = new List<GroupModel>();
        groups = await groupCollection.Find(_ => true).ToListAsync();
        return groups;
    } 



    public static async Task<bool> UpdateGroup(string groupName, string groupId)
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


    public static async Task DeleteMember(string groupId, string memberId) 
    { 
        var filter = Builders<GroupModel>.Filter.Eq(x => x.id, groupId);
        var update = Builders<GroupModel>.Update.Pull(x => x.members, memberId);
        await groupCollection.UpdateOneAsync(filter, update);
        return;
    }

    public static async Task<GroupModelDetail> GetGroupById(string groupId)
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


    public static async Task DeleteGroup(string groupId) 
    {
        var filter = Builders<GroupModel>.Filter.Eq(group => group.id, groupId);
        await groupCollection.DeleteOneAsync(filter);
        return;
    }

    public static async Task AddMembers(string groupId, List<string> memberIds)
    {
        var filter = Builders<GroupModel>.Filter.Eq(x => x.id, groupId);
        var update = Builders<GroupModel>.Update.PushEach(x => x.members, memberIds, position: 0);
        await groupCollection.UpdateOneAsync(filter, update);
        return;
    }

}