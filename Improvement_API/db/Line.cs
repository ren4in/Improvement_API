using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Point = Improvement_API.Point;
namespace Improvement_API
{

    public partial class Line
    {
        [Key]
        //  [JsonIgnore]
        public int? id_Line { get; set; }

        public int? id_Point1 { get; set; }

        public int? id_Point2 { get; set; } = null!;
        public string? Color { get; set; } = null!;

        public virtual Point? id_Point1Navigation { get; set; } = null!;
        public virtual Point? id_Point2Navigation { get; set; } = null!;



    }
}
