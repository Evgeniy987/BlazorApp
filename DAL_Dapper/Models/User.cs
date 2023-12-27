using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Dapper.Models;

public class User : Obj
{

    public string? PhoneNumber { get; set; }
    public string? EmailAddress { get; set; }

    public string? AccessToken { get; set; }

    public int BadgeColor { get; set; }
    public string BadgeText { get => ((Name != null && Name != "") ? Name[..1] : "") + ((Name != null && Name.Contains(' ')) ? Name.Split(" ")[1][..1] : ""); }

    public int? CompanyId { get; set; }

    public bool IsDisabled { get; set; }
    public string? DisabledBy { get; set; }
}