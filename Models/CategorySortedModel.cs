namespace Workdo.Models;

using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Workdo.Helpers;
using System;
public class CategorySortedModel
{

    public string id { get; set; }

    /// <summary> ID của người sỡ hữu </summary>
    public string author { get; set; }


    /// <summary> Danh sách vị trí sắp xếp </summary>
    public List<int> sorted { get; set; }


}