using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TarihWebAPI.ApplicationAndDomain.Enums;
using TarihWebAPI.ApplicationAndDomain.Relationships;

namespace TarihWebAPI.ApplicationAndDomain.Entities
{
    [Table("Works")]
    public class Work : BaseEntity
    {
        [Required]
        [MaxLength(500)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string OriginalTitle { get; set; }

        public WorkType WorkType { get; set; }

        // Birincil yazar — null olabilir (anonim eserler)
        public Guid? AuthorId { get; set; }

        public int? PublicationYear { get; set; }

        [MaxLength(200)]
        public string Language { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [Column(TypeName = "text")]
        public string Summary { get; set; }

        public bool IsReligiousText { get; set; }

        public Guid? ReligionId { get; set; }

        [MaxLength(500)]
        public string Subject { get; set; }

        [Range(1, 10)]
        public int? Importance { get; set; }

        // Eserin ilişkili olduğu tarihi olay (savaş günlüğü, antlaşma belgesi vs.)
        public Guid? RelatedEventId { get; set; }

        [MaxLength(500)]
        public string CoverImageUrl { get; set; }

        [MaxLength(500)]
        public string WikipediaUrl { get; set; }

        // Navigation Properties
        [ForeignKey("AuthorId")]
        public virtual Person Author { get; set; }

        [ForeignKey("ReligionId")]
        public virtual Religion Religion { get; set; }

        [ForeignKey("RelatedEventId")]
        public virtual HistoricalEvent RelatedEvent { get; set; }

        // Birden fazla yazar, çevirmen, editör desteği
        public virtual ICollection<WorkContributor> Contributors { get; set; }
    }
}