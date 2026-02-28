using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TarihWebAPI.ApplicationAndDomain.Entities;

namespace TarihWebAPI.ApplicationAndDomain.Relationships
{
    [Table("ReligionHolyBooks")]
    public class ReligionHolyBook : BaseEntity
    {
        [Required]
        public Guid ReligionId { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string OriginalName { get; set; }

        [MaxLength(100)]
        public string Language { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [MaxLength(500)]
        public string WikipediaUrl { get; set; }


        [ForeignKey("ReligionId")]
        public virtual Religion Religion { get; set; }
    }
}