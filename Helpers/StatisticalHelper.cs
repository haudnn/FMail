using Workdo.Models;
using Workdo.Helpers;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Workdo.Helpers;

public class  StatisticalHelper {

    // condition in here: 
    public static List<MemberModel> getAllMembers = InitDataFakeHelper.InitMembers();
    // public static List<MemberModel> GetMembersUnvote(List<ChoiceModel> choices , List<MemberModel> membersReceived) {
    //     var allVoters = choices.SelectMany(c => c.voters).ToList();
    //     var unvotedMembers = membersReceived.Where(m => !allVoters.Any(v => v.id == m.id)).ToList();
    //     return unvotedMembers;
    // }
}