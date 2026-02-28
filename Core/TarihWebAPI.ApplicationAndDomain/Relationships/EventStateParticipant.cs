using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Enums;

namespace TarihWebAPI.ApplicationAndDomain.Relationships
{
    [Table("EventStateParticipants")]
    public class EventStateParticipant : BaseEntity
    {
        [Required]
        public Guid EventId { get; set; }

        [Required]
        public Guid StateId { get; set; }

        public ParticipantRole Role { get; set; }

        public bool IsMainParticipant { get; set; }  // Ana taraf mı yardımcı mı?

        public bool? IsWinner { get; set; }

        public long? Casualties { get; set; }

        [Column(TypeName = "text")]
        public string Notes { get; set; }

        // Navigation Properties
        [ForeignKey("EventId")]
        public virtual HistoricalEvent Event { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }
    }

}
