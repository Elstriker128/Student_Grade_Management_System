using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Grade_Management_System.Models
{
    public class Administrator
    {
        [Column("admin_user")]
        public string Username { get; set; }
        [Column("slaptazodis")]
        public string Password { get; set; }
        [Column("vardas")]
        public string Name { get; set; }
        [Column("pavarde")]
        public string Surname { get; set; }
        [ForeignKey("fk_MOKYKLAmokyklos_id")]
        public int School_ID { get; set; }
    }
}