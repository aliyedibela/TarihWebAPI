using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TarihWebAPI.ApplicationAndDomain.Enums;
using TarihWebAPI.ApplicationAndDomain.Relationships;

namespace TarihWebAPI.ApplicationAndDomain.Entities
{
    [Table("Persons")]
    public class Person : BaseEntity
    {
        [Required]
        [MaxLength(300)]
        public string FullName { get; set; }

        [MaxLength(300)]
        public string NativeName { get; set; }

        public DateTime? BirthDate { get; set; }

        // Tam tarih bilinmeyip sadece yıl bilindiği durumlar için (antik şahsiyetler)
        public int? BirthYear { get; set; }

        public DateTime? DeathDate { get; set; }

        public int? DeathYear { get; set; }

        public Guid? BirthLocationId { get; set; }

        public Guid? DeathLocationId { get; set; }

        public Gender Gender { get; set; }

        [MaxLength(200)]
        public string Nationality { get; set; }

        // Occupation buradan KALDIRILDI.
        // Bir kişinin birden fazla mesleği olabilir (Sultan + Şair gibi).
        // Bunun yerine PersonOccupations koleksiyonu kullanılacak.

        [MaxLength(500)]
        public string FieldOfWork { get; set; }

        [Column(TypeName = "text")]
        public string ShortBio { get; set; }

        [Column(TypeName = "text")]
        public string DetailedBio { get; set; }

        public Guid? ReligionId { get; set; }

        public Guid? SectId { get; set; }

        public Guid? DynastyId { get; set; }

        public Guid? EraId { get; set; }

        public Guid? PeriodId { get; set; }

        [MaxLength(500)]
        public string ImageUrl { get; set; }

        [MaxLength(500)]
        public string WikipediaUrl { get; set; }

        // Navigation Properties
        [ForeignKey("ReligionId")]
        public virtual Religion Religion { get; set; }

        [ForeignKey("SectId")]
        public virtual Sect Sect { get; set; }

        [ForeignKey("DynastyId")]
        public virtual Dynasty Dynasty { get; set; }

        [ForeignKey("EraId")]
        public virtual Era Era { get; set; }

        [ForeignKey("PeriodId")]
        public virtual Period Period { get; set; }

        [ForeignKey("BirthLocationId")]
        public virtual Location BirthLocation { get; set; }

        [ForeignKey("DeathLocationId")]
        public virtual Location DeathLocation { get; set; }

        // Meslekler — çoklu meslek desteği
        public virtual ICollection<PersonOccupation> Occupations { get; set; }

        // Kişiler arası ilişkiler (baba-oğul, hoca-öğrenci vs.)
        public virtual ICollection<PersonRelation> Relations { get; set; }
        public virtual ICollection<PersonRelation> InverseRelations { get; set; }

        public virtual ICollection<PersonStateRelation> StateRelations { get; set; }
        public virtual ICollection<EventPersonParticipant> EventParticipations { get; set; }

        // Eserlerde birincil yazar olarak
        public virtual ICollection<Work> AuthoredWorks { get; set; }

        // Eserlerde katılımcı olarak (çevirmen, editör vs.)
        public virtual ICollection<WorkContributor> WorkContributions { get; set; }
    }
}