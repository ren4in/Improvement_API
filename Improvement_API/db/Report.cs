using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Json.Serialization;

namespace Improvement_API.db
{
    public class Report
    {
        [Key]
        public int id_Report { get; set; }
        public int id_Order { get; set; }
        public int id_User { get; set; }

        public string Text { get; set; }
        public bool Accepted { get; set; }

        public string Manager_Comment { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime Final_Date { get; set; }
        public string? Header { get; set; }

        public virtual User? id_UserNavigation { get; set; } = null!;



    }

}
