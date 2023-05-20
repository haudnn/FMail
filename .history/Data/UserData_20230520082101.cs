
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
      var isFoundUser = userCollection.Find(u => u.id == id).FirstOrDefault();
      if(isFoundUser == null)
      {
          return null;
      }
      return isFoundUser;
  }



  public static async Task<UserModel> Login(string username, string password)
  {
;
    var isFoundUser = userCollection.Find(u => u.email == username && u.password == password).FirstOrDefault();
    if(isFoundUser == null) 
    {
        return null; 
    }
    return isFoundUser;
  }

	public static async Task<string> Register(UserModel model)
	{
			if(CheckEmail(model.email))
				return "Email này đã được sử dụng! Vui lòng sử dụng 1 email khác";

		  if (string.IsNullOrEmpty(model.id))
        model.id = SharedHelperV2.GenerateID("19012001");
      if (string.IsNullOrEmpty(model.avatar))
        model.avatar = $"https://avatars.dicebear.com/api/micah/{model.id}.svg?background=grey";

      model.email = model.email.Trim().ToLower();

      model.create_date = DateTime.Now.Ticks;
      model.active = true;
      model.delete = false;

      await userCollection.InsertOneAsync(model);

      return "Đăng ký tài khoản thành công!";
	}

    /// <summary>
    /// Kiển tra email đã tồn tại chưa
    /// </summary>
    public static bool CheckEmail(string email)
    {
      if (string.IsNullOrEmpty(email))
        return false;
      else
        email = email.Trim().ToLower();

      var result = userCollection.Find(x => !x.delete && x.email.ToLower() == email).FirstOrDefault();

      return result != null;
    }


	public async Task<List<User>>
}
