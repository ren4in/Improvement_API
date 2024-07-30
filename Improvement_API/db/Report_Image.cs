using Improvement_API;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Improvement_API
{
    public partial class Report_Image
    {
        [Key]
        public int? id_Report_Image { get; set; }
        public int? id_Report { get; set; }
        public Guid RowGuid { get; set; }
        public byte[] Image { get; set; }
        public virtual Report? id_ReportNavigation { get; set; }
 

    }

}
