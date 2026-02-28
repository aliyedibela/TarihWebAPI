using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Enums;

namespace TarihWebAPI.ApplicationAndDomain.Relationships
{
    [Table("PersonOccupations")]
    public class PersonOccupation : BaseEntity
    {
        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public Occupation Occupation { get; set; }

        // Ana meslek mi? (Kanuni için Sultan = true, Şair = false)
        public bool IsPrimary { get; set; }

        // Aktif olduğu yıllar (opsiyonel)
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        // Navigation Properties
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
    }
}