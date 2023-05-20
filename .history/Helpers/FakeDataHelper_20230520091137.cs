using Workdo.Models;
using System.Collections.Generic;
using System;
using Faker.Extensions;
using Workdo.Data;
using System.Threading.Tasks;
namespace Workdo.Helpers;
using System.Linq;
public class FakeDataHelper
{


    /// <summary>
    /// Hàm dùng để chuyển đổi user thành member model
    /// </summary>

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


    /// <summary>
    /// Hàm dùng để khởi tạo danh sách user
    /// </summary>

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

    /// <summary>
    /// Hàm dùng để random avatar cho user
    /// </summary>

    public static string RandomAvatar() 
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 10001);
        return "https://avatars.dicebear.com/api/adventurer-neutral/" + randomNumber + ".svg";
    }



    /// <summary>
    /// Hàm dùng để chuyển đổi members id thành member model
    /// </summary>


    public static async Task<List<MemberModel>> GetMembersById(List<string> ids)
    {
        List<MemberModel> matchingMembers = new List<MemberModel>();
        List<MemberModel> members = await UserData.GetList();
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



    /// <summary>
    /// Hàm dùng để chuyển đổi lấy label bằng ids
    /// </summary>
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


    // Action name: Trả lời, trả lời tất cả, chuyển tiếp
    public static string GetActionName(int action)
    {
        if (action == 1)
            return "Trả lời";
        if (action == 2)
            return "Trả lời tất cả";
        if (action == 3)
            return "Chuyển tiếp";
        return "";
    }


    public static List<FilterModel> InitFilterTimeHelper()
    {
        List<FilterModel> times = new List<FilterModel>();
        times.Add(new FilterModel { id = 1, name = "Tuần này" });
        times.Add(new FilterModel { id = 11, name = "Tuần trước" });
        times.Add(new FilterModel { id = 2, name = "Tháng này" });
        times.Add(new FilterModel { id = 22, name = "Tháng trước" });
        times.Add(new FilterModel { id = 3, name = "Quý này" });
        return times;
    }

    public static List<FilterModel> InitFilterStatusHelper()
    {
        List<FilterModel> status = new List<FilterModel>();
        status.Add(new FilterModel { id = 1, name = "Thư đã đọc" });
        status.Add(new FilterModel { id = 2, name = "Thư chưa đọc" });
        return status;
    }


}