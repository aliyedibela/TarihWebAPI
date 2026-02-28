using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TarihWebAPI.ApplicationAndDomain.Enums;
using TarihWebAPI.ApplicationAndDomain.Relationships;

namespace TarihWebAPI.ApplicationAndDomain.Entities
{
    [Table("Treaties")]
    public class Treaty : BaseEntity
    {
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string OfficialName { get; set; }

        public TreatyType? TreatyType { get; set; }

        // Nullable: Antik antlaşmalarda kesin tarih bilinmeyebilir
        public DateTime? SigningDate { get; set; }

        // SigningDate bilinmeyip sadece yıl bilindiği durumlar için fallback
        public int? SigningYear { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public Guid? SigningLocationId { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [MaxLength(1000)]
        public string Summary { get; set; }

        [Range(1, 10)]
        public int? Importance { get; set; }

        public Guid? EraId { get; set; }

        public Guid? PeriodId { get; set; }

        public Guid? RelatedEventId { get; set; }

        public decimal? EconomicValue { get; set; }

        [MaxLength(50)]
        public string Currency { get; set; }

        [MaxLength(500)]
        public string DocumentUrl { get; set; }

        [MaxLength(500)]
        public string WikipediaUrl { get; set; }

        // Navigation Properties
        [ForeignKey("SigningLocationId")]
        public virtual Location SigningLocation { get; set; }

        [ForeignKey("EraId")]
        public virtual Era Era { get; set; }

        [ForeignKey("PeriodId")]
        public virtual Period Period { get; set; }

        [ForeignKey("RelatedEventId")]
        public virtual HistoricalEvent RelatedEvent { get; set; }

        public virtual ICollection<TreatyArticle> Articles { get; set; }
        public virtual ICollection<TreatySignatory> Signatories { get; set; }
    }
}