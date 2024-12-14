using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Grade_Management_System.Models
{
    public class ParentOfStudent
    {
        [ForeignKey("fk_MOKINYSmokinio_useris")]
        [Column("fk_MOKINYSmokinio_useris")]
        public string Student_Username { get; set; }
        [ForeignKey("fk_TEVAStevo_useris")]
        [Column("fk_TEVAStevo_useris")]
        public string Parent_Username { get; set; }
    }
}