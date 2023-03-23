namespace Workdo.Models;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
public class MailModel
{
    public int id { get; set; }
    // <summary>Email người gửi</summary>
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
    // summary>Ngày nhận</summary>
    public string receivedDate { get; set; }
    // summary>Check đã đọc </summary>     
    public bool isRead { get; set; }
    // summary>Check thư quan trọng </summary>  
    public bool isImportant { get; set; }
    // summary>Check thư nháp </summary>  
    public bool isDraft { get; set; }
    // summary>Check thư rác </summary>  
    public bool isTrash { get; set; }
    // summary>List labels </summary> 
    public List<LabelModel> labels { get; set; }
}

public class AttachmentModel
{
    // <summary>file name</summary>
    public string name { get; set; }
    // <summary>file bytes</summary>
    public byte[] data { get; set; }
    // <summary>type of file</summary> 
    public string contentType { get; set; }
}