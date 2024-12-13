using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Grade_Management_System.Models
{
    public class ReviewType
    {
        [Column("id_atsiliepimo_tipas")]
        public int ID { get; set; }
        [Column("name")]
        public string Name { get; set; }
    }
    public class Review // atsiliepimas
    {
        [Column("atsiliepimo_id")]
        public int ID { get; set; }
        [Column("data")]
        public DateTime Date { get; set; }
        [Column("turinys")]
        public string Content { get; set; }
        [Column("tipas" )]
        public int Type { get; set; }
        [ForeignKey("fk_MOKINYSmokinio_useris")]
        [Column("fk_MOKINYSmokinio_useris")]
        public string Student_Username { get; set; }
        [ForeignKey("fk_MOKYTOJASmokytojo_useris")]
        [Column("fk_MOKYTOJASmokytojo_useris")]
        public string Teacher_Username { get; set; }
    }
    public class ReviewM // atsiliepimas
    {
        [Column("atsiliepimo_id")]
        public int ID { get; set; }
        [Column("data")]
        public DateTime Date { get; set; }
        [Column("turinys")]
        public string Content { get; set; }
        public string Type { get; set; }
        public string Student_Name { get; set; }
        public string Teacher_Name { get; set; }
    }
}