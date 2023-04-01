using MongoDB.Bson.Serialization.Attributes;
using System;
namespace Workdo.Models;

public class CategoryModel
{
    public string id { get; set; }
    public string name { get; set; }

    public int position { get; set; }
    public DateTime createdAt { get; set; }
}