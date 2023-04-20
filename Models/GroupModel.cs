namespace Workdo.Models;
using System.Collections.Generic;

public class GroupModel
{
    public string id { get; set; }
    // <summary> Tên nhóm </summary>
    public string name { get; set; }
    // <summary> list id members in group</summary>
    public List<string> members { get; set; }
    // <summary> Author của nhóm </summary>
    public string author { get; set; }
}




public class GroupModelDetail
{
    public string id { get; set; }
    public string name { get; set; }
    // <summary> list id members in group</summary>
    public List<MemberModel> members { get; set; }
}




public class GroupSelectedModel
{
    public List<GroupModel> groups { get; set; }
    public string name { get; set; }
    public GroupModel group { get; set; }

}
public class MemberSelectedModel
{
    public List<MemberModel> members { get; set; }
    // to or cc or bcc: type
    public string name { get; set; }
}