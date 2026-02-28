using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Enums;

namespace TarihWebAPI.ApplicationAndDomain.Relationships
{
    [Table("WorkContributors")]
    public class WorkContributor : BaseEntity
    {
        [Required]
        public Guid WorkId { get; set; }

        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public ContributorRole Role { get; set; }

        [MaxLength(300)]
        public string Notes { get; set; }

        // Navigation Properties
        [ForeignKey("WorkId")]
        public virtual Work Work { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
    }
}