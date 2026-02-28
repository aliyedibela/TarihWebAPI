using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Enums;

namespace TarihWebAPI.ApplicationAndDomain.Relationships
{
    [Table("EventPersonParticipants")]
    public class EventPersonParticipant : BaseEntity
    {
        [Required]
        public Guid EventId { get; set; }

        [Required]
        public Guid PersonId { get; set; }

        public PersonEventRole Role { get; set; }

        [MaxLength(300)]
        public string RoleDetail { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [ForeignKey("EventId")]
        public virtual HistoricalEvent Event { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
    }
}