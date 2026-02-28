using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TarihWebAPI.ApplicationAndDomain.Entities;

namespace TarihWebAPI.ApplicationAndDomain.Relationships
{
    [Table("ReligionSubBranches")]
    public class ReligionSubBranch : BaseEntity
    {
        [Required]
        public Guid ReligionId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string NameInOriginalLanguage { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public int? FoundedYear { get; set; }

        [MaxLength(500)]
        public string WikipediaUrl { get; set; }

        // Navigation Properties
        [ForeignKey("ReligionId")]
        public virtual Religion Religion { get; set; }
    }
}