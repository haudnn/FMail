namespace Workdo.Models;
using System.Collections.Generic;

public class GroupModel
{
    public string id { get; set; }
    public string name { get; set; }
    public List<MemberModel> members { get; set; }
}