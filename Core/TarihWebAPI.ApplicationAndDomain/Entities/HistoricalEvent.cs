using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TarihWebAPI.ApplicationAndDomain.Enums;
using TarihWebAPI.ApplicationAndDomain.Relationships;

namespace TarihWebAPI.ApplicationAndDomain.Entities
{
    [Table("HistoricalEvents")]
    public class HistoricalEvent : BaseEntity
    {
        [Required]
        [MaxLength(500)]
        public string Title { get; set; }

        public EventType? EventType { get; set; }

        public DateTime? StartDate { get; set; }

        public int? StartYear { get; set; }

        public DateTime? EndDate { get; set; }

        public int? EndYear { get; set; }

        public bool IsExactDate { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [MaxLength(1000)]
        public string ShortDescription { get; set; }

        [Range(1, 10)]
        public int? Importance { get; set; }

        public long? Casualties { get; set; }

        public Guid? LocationId { get; set; }

        public Guid? EraId { get; set; }

        public Guid? PeriodId { get; set; }

        // -----------------------------------------------
        // Hiyerarşi: Üst olay → Alt olaylar
        // Örnek:
        // "1. Dünya Savaşı" (ParentId = null) — üst olay
        //   └─ "Çanakkale Cephesi" (ParentId = WW1.Id)
        //       └─ "Conkbayırı Muharebesi" (ParentId = Çanakkale.Id)
        // -----------------------------------------------
        public Guid? ParentEventId { get; set; }

        [MaxLength(500)]
        public string PrimaryImageUrl { get; set; }

        [MaxLength(500)]
        public string WikipediaUrl { get; set; }

        // Navigation Properties
        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }

        [ForeignKey("EraId")]
        public virtual Era Era { get; set; }

        [ForeignKey("PeriodId")]
        public virtual Period Period { get; set; }

        // Üst olay (örn: Çanakkale'nin parent'ı → 1. Dünya Savaşı)
        [ForeignKey("ParentEventId")]
        public virtual HistoricalEvent ParentEvent { get; set; }

        // Alt olaylar (örn: 1. Dünya Savaşı'nın children'ları → tüm cepheler)
        public virtual ICollection<HistoricalEvent> SubEvents { get; set; }

        public virtual ICollection<EventStateParticipant> StateParticipants { get; set; }
        public virtual ICollection<EventPersonParticipant> PersonParticipants { get; set; }
    }
}