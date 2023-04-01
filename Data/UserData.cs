using System.Security.Cryptography;
using System.Threading.Tasks;
using Workdo.Helpers;
using Workdo.Models;
using System.Collections.Generic;

namespace Workdo.Data;

public class UserData

{

  private static UserModel UserDemo = new UserModel()
  {
    id = "0123456789",
    email = "admin@workdo.com",
    password = "admin@#",
    avatar = "https://avatars.dicebear.com/api/micah/happy.svg?background=pink",
    first_name = "Admin",
    last_name = "Workdo",
    session = "781e5e245d69b566979b86e28d23f2c7"
  };

  public static async Task<UserModel> Get(string id)
  {
      List<UserModel> users = InitDataFakeHelper.InitUsers();
      UserModel user = new();
      var isFoundUser = users.Find(u => u.id == id);
      if(isFoundUser == null)
      {
          return null;
      }
      return isFoundUser;
  }



  public static async Task<UserModel> Login(string username, string password)
  {
    List<UserModel> users = InitDataFakeHelper.InitUsers();
    var isFoundUser = users.Find(u => u.email == username && u.password == password);
    if(isFoundUser == null) 
    {
        return null; 
    }
    await Task.Delay(100);
    return isFoundUser;
  }
}
