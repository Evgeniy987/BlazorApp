using System;
using System.ComponentModel;

namespace DAL_Dapper.Models
{
    public class Obj
    {
        public int Id { get; set; }

        [DisplayName("Заголовок")]
        public string Name { get; set; } = string.Empty;

        public string? LongName { get; set; }

        [DisplayName("Примечания")]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }

        public List<Tag> Tags { get; set; } = new();

        public bool ShowDetails { get; set; }

        // Note: this is important so the MudSelect can compare objects
        public override bool Equals(object obj)
        {
            var other = obj as Obj;
            return other?.Id == Id;
        }

        // Note: this is important too!
        public override int GetHashCode() => Id.GetHashCode();

        // Implement this for the Dealer to display correctly in MudSelect
        public override string ToString() => Name;
    }
}

