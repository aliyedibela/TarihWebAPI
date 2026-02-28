using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TarihWebAPI.ApplicationAndDomain.Relationships;

namespace TarihWebAPI.ApplicationAndDomain.Entities
{
    [Table("Religions")]
    public class Religion : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string NameInOriginalLanguage { get; set; }

        [MaxLength(100)]
        public string ShortName { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public DateTime? FoundedDate { get; set; }

        public int? FoundedYear { get; set; }

        // Kurucu kişi sistemde Person olarak mevcutsa FK ile bağla
        public Guid? FounderPersonId { get; set; }

        // Kurucu sistemde kayıtlı değilse fallback string
        [MaxLength(200)]
        public string FoundedBy { get; set; }

        // Kurulduğu yer sistemde Location olarak mevcutsa FK ile bağla
        public Guid? FoundedLocationId { get; set; }

        // Kurulduğu yer sistemde kayıtlı değilse fallback string
        [MaxLength(200)]
        public string FoundedLocationName { get; set; }

        [MaxLength(100)]
        public string MainBranch { get; set; }

        public long? ApproximateFollowers { get; set; }

        [MaxLength(500)]
        public string IconUrl { get; set; }

        [MaxLength(7)]
        public string ColorCode { get; set; }

        [MaxLength(500)]
        public string WikipediaUrl { get; set; }

        // Navigation Properties
        [ForeignKey("FounderPersonId")]
        public virtual Person FounderPerson { get; set; }

        [ForeignKey("FoundedLocationId")]
        public virtual Location FoundedLocation { get; set; }

        // HolyBooks, HolyCities, SubBranches artık ayrı tablolarda
        public virtual ICollection<ReligionHolyBook> HolyBooks { get; set; }
        public virtual ICollection<ReligionHolyCity> HolyCities { get; set; }
        public virtual ICollection<ReligionSubBranch> SubBranches { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
        public virtual ICollection<Work> ReligiousWorks { get; set; }
        public virtual ICollection<Sect> Sects { get; set; }
    }
}