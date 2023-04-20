namespace Workdo.Models;

using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Workdo.Helpers;
using System;

public class MailModel
{
    public string id { get; set; }

    // <summary>id user from </summary>
    public string from { get; set; }

    // <summary>List các địa chỉ người nhận</summary>
    public List<string> to { get; set; }

    // <summary>List các địa chỉ người nhận cc </summary>
    public List<string> cc { get; set; }

    // <summary>List các địa chỉ người nhận bcc </summary>
    public List<string> bcc { get; set; }

    // <summary>Tiêu đề của email</summary>
    public string subject { get; set; }

    // <summary>Nội dung  của email</summary>
    public string body { get; set; }
    // <summary>Danh sách các file</summary>
    public List<AttachmentModel> attachments { get; set; }
    // summary>Ngày gửi</summary>
    public string sentDate { get; set; }
    // summary>Check đã đọc </summary>     
    public bool isRead { get; set; }
    // summary>Check thư quan trọng </summary>  
    public bool isImportant { get; set; }
    // summary>Check thư nháp </summary>  
    public bool isDraft { get; set; }
    // <summary> folder trước khi xóa </summary> 

    // summary>Check thư rác </summary>  
    public bool isTrash { get; set; }

    // summary>List ids label </summary> 
    public List<string> labels { get; set; }

    // <summary>Check mail reply </summary> 
    public bool isReply { get; set; }
    // <summary>Id của email gốc </summary> 
    public string originalMailId { get; set; }


    // summary> author of mail </summary> 
    public string author { get; set; }

    // <summary> danh muc </summary> 
    public string category { get; set; }

    // s<ummary> Chu ky </summary> 
    public SignatureModel signature { get; set; }
    // <summary> poll id </summary> 
    public string pollId { get; set; }
    // <summary> check xóa trong Draft </summary> 
    public bool isDeleted { get; set; }
    // <summary> for home view </summary> 
    public string shortBody { get; set; }
    public long created_at { get; set; }
    public string parentId { get; set; }

}


public class AttachmentModel
{
    public string id { get; set; }  
    // <summary>file name</summary>
    public string name { get; set; }
    // <summary>type of file</summary> 
    public string contentType { get; set; }
    public long size { get; set; }
    public string filePath { get; set; }
    public string icon => FileHelper.GetIconForFileType(name);
}

public class TagMailModel
{
    public string textColor { get; set; }
    public string backgroundColor { get; set; }
    public string name { get; set; }
}


public class FilterModel
{
    public int id { get; set; }
    public string name { get; set; }

    public string categoryId { get; set; }

    public string startTime { get; set; }
    public string endTime { get; set; }
    public int isRead { get; set; }

    public List<string> labelIds { get; set; }
}
public class TimeModel
{
    public string start { get; set; }
    public string end { get; set; }
}

public class ActionModel
{
    public string mail { get; set; }
    public string action { get; set; }
}