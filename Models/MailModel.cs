namespace Workdo.Models;

using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Workdo.Helpers;
using System;

public class MailModel
{
    public string id { get; set; }

    /// <summary> id người gửi </summary>
    public string from { get; set; }

    /// <summary> Danh sách to </summary>
    public List<string> to { get; set; }

    /// <summary> Danh sách cc </summary>
    public List<string> cc { get; set; }

    /// <summary> Danh sách bcc </summary>
    public List<string> bcc { get; set; }

    /// <summary>Tiêu đề của email</summary>
    public string subject { get; set; }

    /// <summary>Nội dung  của email</summary>
    public string body { get; set; }

    /// <summary>Danh sách các file</summary>
    public List<AttachmentModel> attachments { get; set; } = new List<AttachmentModel>();

    /// <summary>Ngày gửi</summary>
    public string sentDate { get; set; }

    /// <summary> Check đã đọc </summary>     
    public bool isRead { get; set; }

    /// <summary> Check thư quan trọng </summary>  
    public bool isImportant { get; set; }

    /// <summary>Check thư nháp </summary>  
    public bool isDraft { get; set; }
    
    /// <summary>Check thư rác </summary>  
    public bool isTrash { get; set; }

    /// <summary> Danh sách id nhãn </summary> 
    public List<string> labels { get; set; }

    /// <summary> Check mail reply </summary> 
    public bool isReply { get; set; }

    /// <summary> ID của email gốc </summary> 
    public string originalMailId { get; set; }
    
    /// <summary> ID Chủ cùa mail </summary> 
    public string author { get; set; }

    /// <summary> ID danh mục </summary> 
    public string category { get; set; }

    /// <summary> Chữ ký </summary> 
    public SignatureModel signature { get; set; }
    
    /// <summary> ID khảo sát </summary> 
    public string pollId { get; set; }

    /// <summary> Check xóa vĩnh viễn </summary> 
    public bool isDeleted { get; set; }

    /// <summary> Body thu gọn </summary> 
    public string shortBody { get; set; }

    /// <summary> Ngày tạo </summary> 
    public long created_at { get; set; }

    /// <summary> Nội dung mail reply </summary> 
    public string replyBody { get; set; }

}


public class AttachmentModel
{
    public string id { get; set; }  

    /// <summary> Tên của file </summary>
    public string name { get; set; }

    /// <summary> Định dạng của file </summary> 
    public string contentType { get; set; }

    /// <summary> Kích thước của file </summary> 
    public long size { get; set; }

    /// <summary> Đường dẫn của file </summary> 
    public string filePath { get; set; }

    /// <summary> Xác định icon </summary> 
    public string icon => FileHelper.GetIconForFileType(name);
}


/// <summary> Xác định tag mail ở trang home </summary> 
public class TagMailModel
{
    /// <summary> Màu của tag </summary> 
    public string textColor { get; set; }

    /// <summary> Background của atg </summary> 
    public string backgroundColor { get; set; }

    /// <summary> Tên của tag </summary> 
    public string name { get; set; }
}

/// <summary> Dùng để lọc ở trang home </summary> 
public class FilterModel
{
    public int id { get; set; }
    public string name { get; set; }

    /// <summary> ID của danh mục </summary> 
    public string categoryId { get; set; }

    /// <summary> Ngày bắt đầu lọc </summary> 
    public string startTime { get; set; }

    /// <summary> Ngày kết thúc lọc </summary> 
    public string endTime { get; set; }

    /// <summary> Lọc đã đọc hay chưa </summary> 
    public int isRead { get; set; }

    public List<string> labelIds { get; set; }
}

/// <summary> Dùng để xác định hành đông ở trang chi tiết </summary> 
public class ActionModel
{

    /// <summary> ID mail </summary> 
    public string mail { get; set; }

    /// <summary> Tên hành động </summary> 
    public string action { get; set; }
}