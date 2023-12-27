public class UserProfile
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;

    public bool IsDarkMode;
    public int BadgeColor { get; set; }
    public int? CompanyId { get; set; }
}
