using E_Commerce_Website.Core.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Website.Core.Enitities
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public bool IsPublished { get; set; }
        public bool IncludeInMenu { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        // 🔑 Foreign Key
        public int UserId { get; set; }

        // 🔗 Navigation
        public Users? User { get; set; }
    }
}
