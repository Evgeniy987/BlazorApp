namespace DAL_Dapper.Models;

public class Tag
{
    public int Id { get; set; }
    public int ObjId { get; set; }
    public string ObjName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string? CreatedBy { get; set; }

    public string? Title { get; set; }
    public int BadgeColor { get; set; }
    public bool IsRemovable { get; set; }
}

