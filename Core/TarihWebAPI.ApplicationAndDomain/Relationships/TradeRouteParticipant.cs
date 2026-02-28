using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TarihWebAPI.ApplicationAndDomain.Entities;

namespace TarihWebAPI.ApplicationAndDomain.Relationships
{
    [Table("TradeRouteParticipants")]
    public class TradeRouteParticipant : BaseEntity
    {
        [Required]
        public Guid TradeRouteId { get; set; }

        // Devlet katılımcısı — nullable: bireysel katılımcılar için StateId olmayabilir
        public Guid? StateId { get; set; }

        // Bireysel katılımcı (tüccar, seyyah vs.)
        // StateId veya PersonId'den en az biri dolu olmalı
        public Guid? PersonId { get; set; }

        [MaxLength(200)]
        public string Role { get; set; } // "Ana tüccar devlet", "Transit güzergah" vs.

        [Column(TypeName = "text")]
        public string Description { get; set; }

        // Navigation Properties
        [ForeignKey("TradeRouteId")]
        public virtual TradeRoute TradeRoute { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
    }
}