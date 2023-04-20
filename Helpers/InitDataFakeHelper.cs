using Workdo.Models;
using System.Collections.Generic;
using System;
using Faker.Extensions;
namespace Workdo.Helpers;
using System.Linq;
public class InitDataFakeHelper
{
    public static List<CategoryModel> InitCategories()
    {
        List<CategoryModel> categories = new List<CategoryModel>();
        categories.Add(new CategoryModel() { id = "1", name = "Vận hành" });
        categories.Add(new CategoryModel() { id = "2", name = "Nhân sự" });
        categories.Add(new CategoryModel() { id = "3", name = "Kế toán" });
        categories.Add(new CategoryModel() { id = "4", name = "Sales marketing" });
        categories.Add(new CategoryModel() { id = "5", name = "Truyền thông" });
        categories.Add(new CategoryModel() { id = "6", name = "Tổng công ty" });
        return categories;
    }

    public static List<GroupModel> InitGroups()
    {
        List<GroupModel> groups = new List<GroupModel>();
        return groups;
    }


    public static List<MemberModel> InitMembers()
    {
        List<MemberModel> members = new List<MemberModel>();
        List<UserModel> users = InitUsers();
        foreach (UserModel user in users)
        {
            members.Add(new MemberModel
            {
                id = user.id,
                avatar = user.avatar,
                email = user.email,
                name = $"{user.first_name} {user.last_name}",
                last_name = user.last_name,
            });
        }
        return members;
    }

    public static List<MemberModel> InitMembersHasCondition(string userid)
    {
        List<MemberModel> members = new List<MemberModel>();
        List<UserModel> users = InitUsers();
        foreach (UserModel user in users)
        {
            if(user.id == userid) 
            { 
                continue;
            }
            else  
            {
                members.Add(new MemberModel
                {
                    id = user.id,
                    avatar = user.avatar,
                    email = user.email,
                    name = $"{user.first_name} {user.last_name}",
                    last_name = user.last_name
                    
                });
            }

        }
        return members;
    }


    public static List<UserModel> InitUsers() 
    {
        List<UserModel> users = new List<UserModel>();
        for (int i = 1; i < 21; i++) {
            users.Add(new UserModel
            {
                id = i.ToString(),
                email = "admin" + i.ToString() + "@gmail.com",
                password = "admin" + i.ToString(),
                first_name = "Nguyen",
                last_name = "Van" + " "+ i.ToString(),
                avatar = RandomAvatar(),
                session = "781e5e245d69b566979b86e28d23f2c7",
            });
        }
        return users;
    }


    public static string RandomAvatar() 
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 10001);
        return "https://avatars.dicebear.com/api/adventurer-neutral/" + randomNumber + ".svg";
    }


    public static List<MemberModel> GetMembersById(List<string> ids)
    {
        List<MemberModel> matchingMembers = new List<MemberModel>();
        List<MemberModel> members = InitMembers();
        foreach (string id in ids)
        {
            MemberModel member = members.Find(m => m.id == id);
            if (member != null)
            {
                matchingMembers.Add(member);
            }
        }
        return matchingMembers;
    }

    public static List<LabelModel> GetLabelsById(List<string> ids, List<LabelModel> labels)
    {
        List<LabelModel> matchingLabels = new List<LabelModel>();
        if(ids.Count == 0 || labels.Count == 0)
        {
            return matchingLabels; 
        }

        foreach (string id in ids)
        {
            LabelModel label = labels.Find(m => m.id == id);
            if (label != null)
            {
                matchingLabels.Add(label);
            }
        }
        return matchingLabels;
    }



    public static List<MemberModel> GetMembersUnion(List<MemberModel> members, List<MemberModel> membersDetail) 
    {
        var uniqueMembers = new List<MemberModel>();
        uniqueMembers = members.Union(membersDetail)
                           .GroupBy(m => m.id)
                           .Where(g => g.Count() == 1)
                           .Select(g => g.First())
                           .ToList();
        return uniqueMembers;
    }

    public static MemberModel GetMemberById(string id)
    {
     
        List<MemberModel> members = InitMembers();
        return members.Where(m => m.id == id).FirstOrDefault();
    }

}