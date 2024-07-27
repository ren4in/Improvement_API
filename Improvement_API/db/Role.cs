using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Improvement_API
{
    public class Role
    {
        [Key]
        public int id_Role { get; set; }

        public string? Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
