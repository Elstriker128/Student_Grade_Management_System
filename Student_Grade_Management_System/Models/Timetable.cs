using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Grade_Management_System.Models
{
    public class Timetable // tvarkaraštis
    {
        [ForeignKey("fk_PAMOKApamokos_id")]
        [Column("fk_PAMOKApamokos_id")]
        public int Lesson_ID { get; set; }
        [ForeignKey("fk_KLASEraide")]
        [Column("fk_KLASEraide")]
        public string Class_Letter { get; set; }
        [ForeignKey("fk_KLASEkelinta")]
        [Column("fk_KLASEkelinta")]
        public int Class_Number { get; set; }
    }
}