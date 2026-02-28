using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TarihWebAPI.ApplicationAndDomain.Relationships;

namespace TarihWebAPI.ApplicationAndDomain.Entities
{
    [Table("Locations")]
    public class Location : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string NameInOriginalLanguage { get; set; }

        [MaxLength(100)]
        public string Type { get; set; } // City, Region, Country, River, Mountain vs.

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [MaxLength(200)]
        public string ModernName { get; set; }

        [MaxLength(200)]
        public string Country { get; set; }

        [MaxLength(200)]
        public string Region { get; set; }

        // Hiyerarşik lokasyon desteği
        // Örnek: "İstanbul" → ParentLocation: "Marmara Bölgesi" → ParentLocation: "Türkiye"
        public Guid? ParentLocationId { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [MaxLength(500)]
        public string ImageUrl { get; set; }

        [MaxLength(500)]
        public string WikipediaUrl { get; set; }

        // Navigation Properties
        [ForeignKey("ParentLocationId")]
        public virtual Location ParentLocation { get; set; }

        public virtual ICollection<Location> SubLocations { get; set; }
        public virtual ICollection<HistoricalEvent> Events { get; set; }
        public virtual ICollection<Person> BornPersons { get; set; }
        public virtual ICollection<Person> DiedPersons { get; set; }
        public virtual ICollection<TradeRouteWaypoint> TradeRouteWaypoints { get; set; }
    }
}