using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TarihWebAPI.ApplicationAndDomain.Enums;
using TarihWebAPI.ApplicationAndDomain.Relationships;

namespace TarihWebAPI.ApplicationAndDomain.Entities
{
    [Table("States")]
    public class State : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string OfficialName { get; set; }

        [MaxLength(200)]
        public string NameInOriginalLanguage { get; set; }

        public GovernmentType GovernmentType { get; set; }

        public int? StartYear { get; set; }

        public int? EndYear { get; set; }

        // Capital string KALDIRILDI — CapitalLocationId + navigation property yeterli.
        // Eğer başkent sistemde Location olarak tanımlıysa FK kullanılır,
        // tanımlı değilse ayrı bir CapitalName alanı eklendi.
        public Guid? CapitalLocationId { get; set; }

        // Başkent sistemde Location olarak kayıtlı değilse fallback
        [MaxLength(200)]
        public string CapitalName { get; set; }

        public Guid? OfficialReligionId { get; set; }

        // OfficialLanguages string KALDIRILDI → StateOfficialLanguage tablosuna taşındı
        // Currency zaten tek string, sorun yok

        [MaxLength(500)]
        public string Currency { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [MaxLength(500)]
        public string FlagUrl { get; set; }

        [MaxLength(500)]
        public string CoatOfArmsUrl { get; set; }

        [MaxLength(7)]
        public string ColorCode { get; set; }

        [MaxLength(500)]
        public string WikipediaUrl { get; set; }

        // Navigation Properties
        [ForeignKey("OfficialReligionId")]
        public virtual Religion OfficialReligion { get; set; }

        [ForeignKey("CapitalLocationId")]
        public virtual Location CapitalLocation { get; set; }

        public virtual ICollection<StateOfficialLanguage> OfficialLanguages { get; set; }
        public virtual ICollection<Period> Periods { get; set; }
        public virtual ICollection<PersonStateRelation> PersonStateRelations { get; set; }
        public virtual ICollection<EventStateParticipant> EventParticipations { get; set; }
        public virtual ICollection<PopulationData> PopulationData { get; set; }
        public virtual ICollection<StateDynastyRelation> StateDynastyRelations { get; set; }
        public virtual ICollection<TradeRouteParticipant> TradeRouteParticipations { get; set; }
    }
}