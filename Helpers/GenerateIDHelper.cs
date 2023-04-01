using System;
namespace Workdo.Helpers;

public class GenerateIDHelper
{
    public static string GenerateID(string userid)
    {
        var id = "";
        Guid guid = Guid.NewGuid();
        string getFirstGuid = guid.ToString().Split('-')[0];
        DateTime now = DateTime.Now;
        string currentDate = now.ToString("YYYY-MM-dd");
        string month = currentDate.Split('-')[1];
        string dd = currentDate.Split('-')[2];
        if (int.Parse(month) >= 10)
        {
            switch (month)
            {
                case "10":
                    month = "a";
                    break;
                case "11":
                    month = "b";
                    break;
                case "12":
                    month = "c";
                    break;
                default:
                    break;
            }
        }

        id = userid + month + dd + getFirstGuid;
        return id;
    }
}