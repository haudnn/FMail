namespace Workdo.Helpers;
using Workdo.Models;
using Workdo.Data;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Globalization;

public class MailHelper
{
    public static List<TagMailModel> TagsMailHelper(string  isPoll, bool isDraft, long countReply)
    {
        List<TagMailModel> tags = new List<TagMailModel>();
        if (!string.IsNullOrEmpty(isPoll))
        {
            tags.Add(new TagMailModel
            {
                textColor = "#141b2c",
                backgroundColor = "#fff962",
                name = "KS"
            });
        }
        if (isDraft)
        {
            tags.Add(new TagMailModel
            {
                textColor = "#fff",
                backgroundColor = "#ffb4a9",
                name = "Thư nháp"
            });
        }
        if (countReply > 0)
        {
            tags.Add(new TagMailModel
            {
                textColor = "#fff",
                backgroundColor = "#c0c6dc",
                name = "Thư đến"
            });
        }

        return tags;
    }


    public static List<MailModel> SearchingHelper(string folder, string data, List<MailModel> mails)
    {
        switch (folder)
        {
            case "sent":
            case "draft":
                //return mails.Where(m => m.subject.Contains(data) || m.shortBody.Contains(data) || m.to.Any(t => t.name.Contains(data))).ToList();
                return mails.Where(m => m.subject.Contains(data) || m.shortBody.Contains(data)).ToList();
            case "inbox":
            case "trash":
                return mails.Where(m => m.subject.Contains(data) || m.shortBody.Contains(data)).ToList();
            case "important":
                // return mails.Where(m => m.subject.Contains(data) || m.shortBody.Contains(data) || m.from.Contains(data) || m.to.Any(t => t.name.Contains(data))).ToList();
                return mails.Where(m => m.subject.Contains(data) || m.shortBody.Contains(data)).ToList();
            default:
                return new List<MailModel>();
        }
    }


    public static List<FilterModel> InitFilterTimeHelper()
    {
        List<FilterModel> times = new List<FilterModel>();
        times.Add(new FilterModel { id = 1 , name = "Tuần này"});
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


    public static List<MailModel> FilterByTimeHelper(string startDate, string endDate, List<MailModel> mails)
    {
        DateTimeOffset start = DateTimeOffset.ParseExact(startDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
        DateTimeOffset end = DateTimeOffset.ParseExact(endDate, "yyyy/MM/dd", CultureInfo.InvariantCulture).AddDays(1).AddSeconds(-1);

        return mails.Where(m =>
            DateTimeOffset.ParseExact(m.sentDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) >= start &&
            DateTimeOffset.ParseExact(m.sentDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) <= end).ToList();
    }

    // Action name: Trả lời, trả lời tất cả, chuyển tiếp
    public static string GetActionName(int action)
    {
        if(action == 1)
            return "Trả lời";
        if (action == 2)
            return "Trả lời tất cả";
        if (action == 3)
            return "Chuyển tiếp";
        return "";
    }

}