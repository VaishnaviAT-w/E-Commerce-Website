using E_Commerce_Website.Core.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Website.Core.Enitities
{
    [Table("Brands")]
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        public string? BrandName { get; set; }
        public bool IsPublished { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }

        // 🔑 Foreign Key
        public int UserId { get; set; }

        // 🔗 Navigation
        [ForeignKey(nameof(UserId))]
        public Users? User { get; set; }

    }
}
