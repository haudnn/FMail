namespace Workdo.Models;

public class SignatureModel
{
    public string id { get; set; }

    /// <summary> Tên chữ ký </summary> 
    public string name { get; set; }

    /// <summary> Nội dung chữ ký </summary> 
    public string body { get; set; }

    /// <summary> Kiểm tra chữ ký mặc định </summary> 
    public bool isDefault { get; set; }
    
    // <summary> Chủ sở hữu của chữ ký  </summary> 
    public string author { get; set; }
}