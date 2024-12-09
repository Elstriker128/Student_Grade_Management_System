using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Grade_Management_System.Models
{
    public class WorkDay
    {
        [Column("id_darbo_diena")]
        public int ID { get; set; }
        [Column("name")]
        public string Name { get; set; }
    }
}