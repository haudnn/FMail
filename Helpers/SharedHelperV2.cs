namespace Workdo.Helpers;
using Workdo.Models;
using Workdo.Data;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.Forms;


public class SharedHelperV2
{
    private static bool isMacOS = Environment.CurrentDirectory.Contains("/");

    /// <summary> Hàm xử lý việc tạo ra các tag của mail </summary>
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

    /// <summary> Hàm dùng để tìm kiếm </summary>
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

    

    /// <summary> Hàm dùng để lọc mail bằng thời gian </summary>
    public static List<MailModel> FilterByTimeHelper(string startDate, string endDate, List<MailModel> mails)
    {
        DateTimeOffset start = DateTimeOffset.ParseExact(startDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
        DateTimeOffset end = DateTimeOffset.ParseExact(endDate, "yyyy/MM/dd", CultureInfo.InvariantCulture).AddDays(1).AddSeconds(-1);

        return mails.Where(m =>
            DateTimeOffset.ParseExact(m.sentDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) >= start &&
            DateTimeOffset.ParseExact(m.sentDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) <= end).ToList();
    }



    /// <summary>
    /// Hàm dùng format: Nếu ngày hiện tại = thì sẽ hiện thị HH:mm nếu không sẽ hiển thị ngày
    /// </summary>
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


    /// <summary>
    ///  Hàm dùng để format datetime về dạng: "2023-02-22 12:30:00"
    /// </summary>
    public static string FormatDateTimeUTC()
    {
        DateTime utcTime = DateTime.UtcNow;
        TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZone);
        return localTime.ToString("yyyy-MM-dd HH:mm:ss");
    }



    /// <summary>
    /// Hàm dùng để lưu file
    /// </summary>

    public static async Task<string> SaveFileAsync(StreamContent inputStream, string fileName)
    {
        string folder = "upload\\";
        string filePath = Environment.CurrentDirectory + "\\wwwroot\\" + folder;

        if (isMacOS)
            filePath = filePath.Replace("\\", "/");

        if (!Directory.Exists(filePath))
            Directory.CreateDirectory(filePath);

        string format = new FileInfo(fileName.ToLower()).Extension;

        string rename = DateTime.Now.Ticks + format;

        string path = Path.Combine(filePath, rename);

        await using FileStream fs = new(path, FileMode.Create);
        await inputStream.CopyToAsync(fs);
        fs.Dispose();
        inputStream.Dispose();

        return $"/{folder.Replace("\\", "/")}/{rename}";
    }



    /// <summary>
    /// Hàm dùng để xóa file bằng đường dẫn
    /// </summary>

    public static void DeleteFile(string filePath)
    {
        string absoluteFilePath = $"{Environment.CurrentDirectory}/wwwroot{filePath.Replace("/", "\\")}";
        if (File.Exists(absoluteFilePath))
        {
            File.Delete(absoluteFilePath);
        }
    }

    /// <summary>
    /// Hàm dùng để kiểm tra file có hợp lệ hay không
    /// </summary>

    public static bool IsValidFileType(IBrowserFile file)
    {
        var allowedExtensions = new[] { ".pdf", ".png", ".jpg", ".jpeg", ".doc", ".docx", ".xls", ".xlsx" };
        var fileExtension = Path.GetExtension(file.Name);
        return allowedExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    ///  Hàm dùng để lấy hình ảnh từ đuôi của file
    /// </summary>
    public static string GetIconForFileType(string name)
    {
        var extension = Path.GetExtension(name)?.ToLowerInvariant();
        switch (extension)
        {
            case ".doc":
            case ".docx":
                return "word-icon";
            case ".pdf":
                return "pdf-icon";
            case ".xls":
            case ".xlsx":
                return "excel-icon";
            case ".png":
            case ".jpg":
            case ".jpeg":
                return "image-icon";
            default:
                return "";
        }
    }


    /// <summary>
    /// Hàm dùng để sinh ra id ngẫu nhiên
    /// </summary>

    public static string GenerateID(string customString)
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

        id = customString + month + dd + getFirstGuid;
        return id;
    }

}