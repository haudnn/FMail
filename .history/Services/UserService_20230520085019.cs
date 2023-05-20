using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using Workdo.Data;
using Workdo.Models;

namespace Workdo.Services
{
  public class UserService
  {
    /// <summary>
    /// Chuyển UserModel thành MemberModel
    /// </summary>
    public static MemberModel ConvertToMember(UserModel user)
    {
      return new MemberModel()
      {
        id = user.id,
        name = user.FullName,
        email = user.email,
        avatar = user.avatar,
      };
    }

    /// <summary>
    /// Lấy 1 tài khoản trong danh sách tài khoản theo ID
    /// </summary>
    public static UserModel GetUser(List<UserModel> list, string userId)
    {
      var user = list.SingleOrDefault(x => x.id == userId);
      if (user != null)
        return user;
      else
        return new UserModel()
        {
          //id = userId,
          last_name = "Tài khoản đã xóa",a
          avatar = "/images/avatar.png"
        };
    }

		public static List<MemberModel> ConvertToMembers(List<UserModel> users)
    {
        List<MemberModel> members = new List<MemberModel>();
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
  }
}