using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Student_Grade_Management_System.Models
{
    public class GradeWeight 
    {
        [Column("svorio_id")]
        public int ID { get; set; }
        [Column("svoris")]
        public double Weight { get; set; }
        [Column("tipas")]
        public int Type { get; set; }
    }

    public class GradeType
    {
        [Column("id_ivertinimo_tipas")]
        public int ID { get; set; }
        [Column("name")]
        public string Name { get; set; }
    }
    public class Grade
    {
        [Column("ivertinimo_id")]
        public int ID { get; set; }
        [Column("pazymys")]
        public int Value { get; set; }
        [Column("data")]
        public DateOnly Date { get; set; }
        [ForeignKey("fk_MOKINYSmokinio_useris")]
        public string Student_Username { get; set; }
        [ForeignKey("fk_PAMOKApamokos_id")]
        public int Subject_ID { get; set; }
        [ForeignKey("fk_IVERTINIMO_SVORISsvorio_id")]
        public int Weight_ID { get; set; }


    }
}