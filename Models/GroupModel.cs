namespace Workdo.Models;
using System.Collections.Generic;

public class GroupModel
{
    public string id { get; set; }
    public string name { get; set; }
    // <summary> list id members in group</summary>
    public List<string> members { get; set; }
}

public class GroupModelDetail
{
    public string id { get; set; }
    public string name { get; set; }
    // <summary> list id members in group</summary>
    public List<MemberModel> members { get; set; }
}