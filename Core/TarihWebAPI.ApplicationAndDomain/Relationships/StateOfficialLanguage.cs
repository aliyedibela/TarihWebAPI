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

        public bool IsPrimary { get; set; }

        public int? StartYear { get; set; }
        public int? EndYear { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }
    }
}