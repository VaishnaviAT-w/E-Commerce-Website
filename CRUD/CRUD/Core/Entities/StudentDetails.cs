using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD.Core.Entities
{
    [Table("StudentDetails")]
    public class StudentDetails
    {
        [Key]
        public int StudentId { get; set; }

        public string? StudentName { get; set; }

        public string? CollegeName { get; set; }

        public int YearOfPassOut { get; set; }

        public decimal Percentage { get; set; }

        public bool IsActive { get; set; } = true;

        public int CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

    }
}
