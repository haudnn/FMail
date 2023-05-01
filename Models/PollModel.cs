namespace Workdo.Models;
using System.Collections.Generic;
using Workdo.Helpers;
public class PollModel
{
    public string id { get; set; }

    /// <summary>Tiêu đề khảo sát </summary>  
    public string title { get; set; }

    /// <summary>Danh sách câu hỏi </summary>  
    public List<QuestionModel> questions { get; set; }

    /// <summary> Thời gian kết thúc poll</summary>
    public string endTime { get; set; }

    /// <summary> Ngày kết thúc poll</summary>
    public string endDate { get; set; }

    /// <summary> Kiểm tra đã dừng khảo sát </summary>
    public bool isStopped { get; set; }

    /// <summary> Người tạo khảo sát </summary>
    public string author { get; set; }
}

public class QuestionModel
{
    public string id { get; set; }

    /// <summary> Tên câu hỏi </summary>  
    public string text { get; set; }

    /// <summary> List các lựa chọn</summary>  
    public List<ChoiceModel> choices { get; set; }

    /// <summary> 1 đáp án hoặc nhiều đáp án: 0 = 1 đáp án - 1 = nhiều đáp án </summary>  
    public bool isMultipleChoice { get; set; }
}

public class ChoiceModel
{
    public string id { get; set; }

    /// <summary> Nội dung lựa chọn</summary>
    public string text { get; set; }

    /// <summary> List người bình chọn </summary>  
    public List<string> voters { get; set; }

}

public class QuestionChoice
{
    public string id { get; set; }
    public bool isMultipleChoice { get; set; }
    public bool isVotePrevious { get; set; }
    public string choiceId { get; set; }
}
