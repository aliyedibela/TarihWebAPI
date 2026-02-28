using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TarihWebAPI.ApplicationAndDomain.Entities;

namespace TarihWebAPI.ApplicationAndDomain.Relationships
{
    [Table("StateOfficialLanguages")]
    public class StateOfficialLanguage : BaseEntity
    {
        [Required]
        public Guid StateId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Language { get; set; }

        // Birincil dil mi?
        public bool IsPrimary { get; set; }

        // Bu dilin resmi olduğu yıllar (devlet tarih boyunca dil değiştirmiş olabilir)
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }

        // Navigation Properties
        [ForeignKey("StateId")]
        public virtual State State { get; set; }
    }
}