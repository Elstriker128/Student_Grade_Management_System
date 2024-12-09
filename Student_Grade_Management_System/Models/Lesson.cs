using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Student_Grade_Management_System.Models
{
    public class Lesson
    {
        [Column("pamokos_id")]
        public int ID { get; set; }
        [Column("pradzios_laikas")]
        public string StartTime { get; set; }
        [Column("pabaigos_laikas")]
        public string EndTime { get; set; }
        [Column("diena")]
        public int Day { get; set; }
        [Column("fk_MOKYTOJASmokytojo_useris")]
        public string Teacher_Username { get; set; }
        [Column("fk_DALYKASdalyko_id")]
        public int Subject_ID { get; set; }
    }
}