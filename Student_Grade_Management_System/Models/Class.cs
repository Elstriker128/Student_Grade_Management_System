using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Grade_Management_System.Models
{
    public class Class 
    {
        [Column("kelinta")]
        public int Number { get; set; }
        [Column("raide")]
        public string Letter { get; set; }
        [Column("mokiniu_skaicius")]
        public int StudentCount { get; set; }
        [Column("atsakingo_mokytojo_useris")]
        public string Teacher_Username { get; set; }
    }
}