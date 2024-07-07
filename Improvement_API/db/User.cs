using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Json.Serialization;

namespace Improvement_API.db
{

    public partial class User
    {
        [Key]
        public int id_User { get; set; }

        public int? id_Role { get; set; }

        public string? FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string? LastName { get; set; } = null!;

        public string? Login { get; set; } = null!;

        public string? Password { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

        [JsonIgnore]
        public virtual Role? id_RoleNavigation { get; set; } = null!;



    }
}
