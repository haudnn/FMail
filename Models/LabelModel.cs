namespace Workdo.Models;

public class LabelModel
{
    /// <summary> id </summary>
    public string id;

    /// <summary> Tên nhãn </summary>
    public string name;

    /// <summary> Màu nhãn </summary>
    public string color;

    /// <summary> Người tạp nhãn </summary>
    public string author;

    /// <summary> Active nhãn </summary>
    public bool active { get; set; }

}