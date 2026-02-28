using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TarihWebAPI.ApplicationAndDomain.Relationships;

namespace TarihWebAPI.ApplicationAndDomain.Entities
{
    [Table("TradeRoutes")]
    public class TradeRoute : BaseEntity
    {
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string AlternativeName { get; set; }

        [Required]
        public int StartYear { get; set; }

        public int? EndYear { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [Column(TypeName = "text")]
        public string MainGoods { get; set; }

        // Başlangıç lokasyonu
        public Guid? StartLocationId { get; set; }

        // Bitiş lokasyonu
        public Guid? EndLocationId { get; set; }

        // RouteGeometry: WKT (Well-Known Text) formatında saklanıyor.
        // PostGIS/NetTopologySuite kullanıyorsanız tipi değiştirin.
        // Şimdilik string olarak saklanıp uygulama katmanında parse edilebilir.
        // YENİ — PostgreSQL için text kullan
        [Column(TypeName = "text")]
        public string RouteGeometryWkt { get; set; }

        [Range(1, 10)]
        public int? Importance { get; set; }

        [MaxLength(500)]
        public string MapUrl { get; set; }

        [MaxLength(500)]
        public string WikipediaUrl { get; set; }

        // Navigation Properties
        [ForeignKey("StartLocationId")]
        public virtual Location StartLocation { get; set; }

        [ForeignKey("EndLocationId")]
        public virtual Location EndLocation { get; set; }

        public virtual ICollection<TradeRouteParticipant> Participants { get; set; }

        // Güzergah üzerindeki lokasyonlar (sıralı)
        public virtual ICollection<TradeRouteWaypoint> Waypoints { get; set; }
    }
}