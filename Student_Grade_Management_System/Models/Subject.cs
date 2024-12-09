using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Grade_Management_System.Models
{
    public class Subject // dalykas
    {
        [Column("dalyko_id")]
        public int ID { get; set; }
        [Column("pavadinimas")]
        public string Name { get; set; }
    }
}