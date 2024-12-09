using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Grade_Management_System.Models
{
    public class Parent
    {
        [Column("tevo_useris")]
        public string Username { get; set; }
        [Column("slaptazodis")]
        public string Password { get; set; }
        [Column("vardas")]
        public string Name { get; set; }
        [Column("pavarde")]
        public string Surname { get; set; }
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
        [Column("butas")]
        public string Apartment { get; set; }
    }
}