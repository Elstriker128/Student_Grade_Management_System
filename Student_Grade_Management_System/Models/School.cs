using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Grade_Management_System.Models
{
    public class School
    {
        [Column("mokyklos_id")]
        public int ID { get; set; }
        [Column("pavadinimas")]
        public string Name { get; set; }
        [Column("telefono_nr")]
        public string PhoneNumber { get; set; }
        [Column("el_pastas")]
        public string Email { get; set; }
        [Column("miestas")]
        public string City { get; set; }
        [Column("gatve")]
        public string Street { get; set; }
        [Column("namas")]
        public string Building { get; set; } // namas

    }
}