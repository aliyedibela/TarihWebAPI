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

        public Guid? StateId { get; set; }

        public Guid? PersonId { get; set; }

        [MaxLength(200)]
        public string Role { get; set; } 

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [ForeignKey("TradeRouteId")]
        public virtual TradeRoute TradeRoute { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
    }
}