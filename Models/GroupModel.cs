namespace Workdo.Models;
using System.Collections.Generic;

public class GroupModel
{
    public string id { get; set; }

    /// <summary> Tên nhóm </summary>
    public string name { get; set; }
    
    /// <summary> Danh sách members id </summary>
    public List<string> members { get; set; }

    /// <summary> Người tạo nhóm </summary>
    public string author { get; set; }
}




public class GroupModelDetail
{
    public string id { get; set; }

    /// <summary> Tên nhóm </summary>
    public string name { get; set; }

    /// <summary> Danh sách members </summary>
    public List<MemberModel> members { get; set; }
}




public class MemberSelectedModel
{
    /// <summary> Danh sách members </summary>
    public List<MemberModel> members { get; set; }

    /// <summary> Xác định CC TO BCC </summary>
    public string name { get; set; }
}