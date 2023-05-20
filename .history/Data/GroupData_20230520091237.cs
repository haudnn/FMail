using Workdo.Helpers;
using Workdo.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Workdo.Services;


public class GroupData {

    public static IMongoCollection<GroupModel> groupCollection = ConnectDB<GroupModel>.GetClient("mailbox", "group");


    public static async Task<bool> Create(GroupModel group)
    {
        var existingGroup = await groupCollection.Find(x => x.name == group.name && x.author == group.author).FirstOrDefaultAsync();
        if (existingGroup == null)
        {
            await groupCollection.InsertOneAsync(group);
            return true;
        }
        return false;
    }

    public static async Task<List<GroupModel>> GetList(string author) 
    { 
        return await groupCollection.Find(x => x.author == author).ToListAsync();
    }


    public static async Task<bool> Update(string groupName, string groupId, string author)
    {
        var existingGroup = await groupCollection.Find(x => x.name == groupName && x.author == author).FirstOrDefaultAsync();
        if (existingGroup == null)
        {
            var filter = Builders<GroupModel>.Filter.Eq(x => x.id, groupId);
            var update = Builders<GroupModel>.Update.Set(g => g.name, groupName);
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

    public static async Task<GroupModelDetail> Get(string groupId)
    {
        var isFoundGroup = await groupCollection.Find(x => x.id == groupId).FirstOrDefaultAsync();
        List<MemberModel> members = await UserService.GetMembersById(isFoundGroup.members);

        GroupModelDetail groupUpdating = new GroupModelDetail{
            id = isFoundGroup.id,
            name = isFoundGroup.name,
            members = members
        };
        
        return groupUpdating;
    }


    public static async Task Delete(string groupId) 
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