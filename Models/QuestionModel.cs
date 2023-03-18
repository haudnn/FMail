using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
namespace Workdo.Models;

public class QuestionModel
{
    // Tên câu hỏi
    public string name { get; set; }
    // 1: Một đáp án
    // 2: Nhiều đáp án
    public int typeOfQuestion { get; set; }

    // Lựa chọn
    public List<OptionModel> optionsList { get; set; }
}
