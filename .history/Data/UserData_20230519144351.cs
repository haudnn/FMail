
using System.Threading.Tasks;
using Workdo.Helpers;
using Workdo.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using System;


namespace Workdo.Data;

public class UserData

{

	public static IMongoCollection<UserModel> userCollection = ConnectDB<UserModel>.GetClient("mailbox", "users");
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

	public static async Task<UserModel> Register(UserModel model)
	{
		  if (string.IsNullOrEmpty(model.id))
        model.id = SharedHelperV2.GenerateID("19012001");
      if (string.IsNullOrEmpty(model.avatar))
        model.avatar = $"https://avatars.dicebear.com/api/micah/{model.id}.svg?background=grey";

      model.email = model.email.Trim().ToLower();

      model.create_date = DateTime.Now.Ticks;
      model.active = true;
      model.delete = false;

      await userCollection.InsertOneAsync(model);

      return model;
	}
}
