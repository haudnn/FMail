using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Workdo.Helpers;
using Workdo.Models;
using System.Collections.Generic;

namespace Workdo.Data;

public class UserData

{

  public static async Task<UserModel> Get(string id)
  {
      List<UserModel> users = FakeDataHelper.InitUsers();
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
    List<UserModel> users = FakeDataHelper.InitUsers();
    var isFoundUser = users.Find(u => u.email == username && u.password == password);
    if(isFoundUser == null) 
    {
        return null; 
    }
    await Task.Delay(100);
    return isFoundUser;
  }
}
