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

        // Enum ile standart rol — EventStateParticipant ile tutarlı
        public PersonEventRole Role { get; set; }

        // Detaylı açıklama için serbest alan ("3. Ordu Komutanı" gibi)
        [MaxLength(300)]
        public string RoleDetail { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        // Navigation Properties
        [ForeignKey("EventId")]
        public virtual HistoricalEvent Event { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
    }
}