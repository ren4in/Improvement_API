using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

using Improvement_API;


namespace Improvement_API
{
    public partial class Point
    {
        [Key]
        public int? id_Point { get; set; } = null!;
        public double? X { get; set; } = null!;
        public double? Y { get; set; } = null!;
        public int? Image { get; set; } = null!;
        public int? id_User { get; set; } = null!;
        public virtual Point_Image? id_Point_ImageNavigation { get; set; } = null!;
        public virtual User? id_UserNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Line> Lines1 { get; set; } = new List<Line>();
        [JsonIgnore]
        public virtual ICollection<Line> Lines2 { get; set; } = new List<Line>();



    }
}
