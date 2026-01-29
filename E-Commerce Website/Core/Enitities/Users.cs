using E_Commerce_Website.Core.Enitities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Website.Core.Entity
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? Mobile { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifyOn { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }

        public ICollection<Brand>? Brands { get; set; }
        public ICollection<Category>? Categories { get; set; }
    }
}
