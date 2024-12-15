using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Grade_Management_System.Models
{
    public class ParentOfStudent
    {
        [Column("fk_MOKINYSmokinio_useris")]
        public string Student_Username { get; set; }

        [Column("fk_TEVAStevo_useris")]
        public string Parent_Username { get; set; }
    }
}
