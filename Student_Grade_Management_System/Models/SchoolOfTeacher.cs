using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Grade_Management_System.Models
{
    public class SchoolOfTeacher
    {
        [ForeignKey("fk_MOKYKLAmokyklosid")]
        [Column("fk_MOKYKLAmokyklosid")]
        public int School_ID { get; set; }
        [ForeignKey("fk_MOKYTOJASmokytojo_useris")]
        [Column("fk_MOKYTOJASmokytojo_useris")]
        public string Teacher_Username { get; set; }
    }
}