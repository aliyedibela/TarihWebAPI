using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarihWebAPI.ApplicationAndDomain.Enums;

namespace TarihWebAPI.ApplicationAndDomain.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MaxLength(500)]
        public string PasswordHash { get; set; }

        [MaxLength(200)]
        public string FullName { get; set; }

        public UserRole Role { get; set; }

        [MaxLength(500)]
        public string ProfileImageUrl { get; set; }

        public DateTime? LastLoginAt { get; set; }

        public bool IsEmailVerified { get; set; }

        public virtual ICollection<UserInteraction> Interactions { get; set; }
    }

}
