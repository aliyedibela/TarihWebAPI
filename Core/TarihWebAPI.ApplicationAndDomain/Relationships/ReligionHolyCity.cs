using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TarihWebAPI.ApplicationAndDomain.Entities;

namespace TarihWebAPI.ApplicationAndDomain.Relationships
{
    [Table("ReligionHolyCities")]
    public class ReligionHolyCity : BaseEntity
    {
        [Required]
        public Guid ReligionId { get; set; }

        public Guid? LocationId { get; set; }

        [Required]
        [MaxLength(200)]
        public string CityName { get; set; }

        [MaxLength(100)]
        public string Significance { get; set; } 

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [ForeignKey("ReligionId")]
        public virtual Religion Religion { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
    }
}