using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TarihWebAPI.ApplicationAndDomain.Relationships;

namespace TarihWebAPI.ApplicationAndDomain.Entities
{
    [Table("Dynasties")]
    public class Dynasty : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string NameInOriginalLanguage { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public int? FoundingYear { get; set; }

        public int? EndYear { get; set; }

        // Kurucu kişi sistemde Person olarak mevcutsa FK ile bağla
        public Guid? FounderPersonId { get; set; }

        // Kurucu sistemde kayıtlı değilse veya belirsizse fallback string
        [MaxLength(200)]
        public string FoundedBy { get; set; }

        [MaxLength(200)]
        public string OriginLocation { get; set; }

        [MaxLength(500)]
        public string CoatOfArmsUrl { get; set; }

        [MaxLength(7)]
        public string ColorCode { get; set; }

        [MaxLength(500)]
        public string WikipediaUrl { get; set; }

        // Navigation Properties
        [ForeignKey("FounderPersonId")]
        public virtual Person FounderPerson { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
        public virtual ICollection<StateDynastyRelation> StateDynastyRelations { get; set; }
    }
}