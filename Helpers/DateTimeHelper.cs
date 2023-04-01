namespace Workdo.Helpers;
using System.Collections.Generic;
using System;

public class DateTimeHelper
{
    public static string FormatDateTime(string date)
    {
        DateTime dateTime = DateTime.Parse(date);
        TimeSpan timeSpan = DateTime.Now.Subtract(dateTime);

        if (timeSpan.TotalDays >= 1)
        {
            return dateTime.ToString("dd/MM/yyyy");
        }
        else
        {
            return dateTime.ToString("HH:mm");
        }
    }



    public static string FormatDateTimeUTC() 
    {
        DateTime utcTime = DateTime.UtcNow;
        TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZone);
        return localTime.ToString("yyyy-MM-dd HH:mm:ss");
    }


    public static string FormatDateTimeDetailView(string date)
    {
        DateTime dateTime = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss", null);
        string formattedString = dateTime.ToString("HH:mm, dd 'thg' M, yyyy");
        return formattedString;
    }
}