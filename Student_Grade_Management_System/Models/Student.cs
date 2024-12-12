using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Grade_Management_System.Models
{
    public class Student 
    {
        [Column("mokinio_useris")]
        public string Username { get; set; }
        [Column("slaptazodis")]
        public string Password { get; set; }
        [Column("vardas")]
        public string Name { get; set; }
        [Column("pavarde")]
        public string Surname { get; set; }
        [Column("gimimo_data")]
        public DateOnly Birth_Date { get; set; }
        [Column("asmens_kodas")]
        public int SSN { get; set; } // asmens kodas
        [Column("miestas")]
        public string City { get; set; }
        [Column("gatve")]
        public string Street { get; set; }
        [Column("namas")]
        public string Building { get; set; } // namas
        [Column("butas")]
        public string Apartment { get; set; }
        //[Column("prisijungimai")]
        //public int LoginCount { get; set; }
        [ForeignKey("fk_MOKYKLAmokyklos_id")]
        public int School_ID { get; set; }
        [ForeignKey("fk_KLASEraide")]
        public string Class_Letter { get; set; }
        [ForeignKey("fk_KLASEkelinta")]
        public int Class_Number { get; set; }
    }
}