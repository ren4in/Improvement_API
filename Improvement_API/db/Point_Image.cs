using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Improvement_API
{
    public partial class Point_Image
    {
        [Key]
        public int? id_Point_Image { get; set; }
        public string Name { get; set; }
        public Guid Point_Image_GUID { get; set; }
        public byte[] Image { get; set; }
        [JsonIgnore]
        public virtual ICollection<Point> Points { get; set; } = new List<Point>();


    }

}
