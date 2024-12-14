using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Grade_Management_System.Models
{
    public class SubjectOfTeacher
    {
        [ForeignKey("fk_DALYKASdalyko_id")]
        [Column("fk_DALYKASdalyko_id")]
        public int Subject_ID { get; set; }
        [ForeignKey("fk_MOKYTOJASmokytojo_useris")]
        [Column("fk_MOKYTOJASmokytojo_useris")]
        public string Teacher_Username { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}