using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TarihWebAPI.ApplicationAndDomain.Entities;

namespace TarihWebAPI.ApplicationAndDomain.Relationships
{
    [Table("TradeRouteWaypoints")]
    public class TradeRouteWaypoint : BaseEntity
    {
        [Required]
        public Guid TradeRouteId { get; set; }

        [Required]
        public Guid LocationId { get; set; }

        // Güzergah sırası (1 = başlangıca yakın, büyük = bitişe yakın)
        [Required]
        public int OrderNumber { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        // Navigation Properties
        [ForeignKey("TradeRouteId")]
        public virtual TradeRoute TradeRoute { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
    }
}