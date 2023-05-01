using MongoDB.Bson.Serialization.Attributes;
using System;
namespace Workdo.Models;

public class CategoryModel
{

    public string id { get; set; }

    /// <summary>Tên danh mục </summary>
    public string name { get; set; }
    
    public int position { get; set; }

    /// <summary>Ngày tạo</summary>
    public long created_at { get; set; }
}