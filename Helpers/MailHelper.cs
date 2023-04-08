namespace Workdo.Helpers;
using Workdo.Models;
using System.Collections.Generic;
using System;

public class MailHelper
{
    public static List<TagMailModel> TagsMailHelper(bool isPoll, bool isDraft, bool isReply)
    {
        List<TagMailModel> tags = new List<TagMailModel>();
        if (isPoll)
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
        if (isReply)
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
}