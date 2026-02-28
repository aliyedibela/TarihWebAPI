using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarihWebAPI.ApplicationAndDomain.Entities
{
    [Table("Sects")]
    public class Sect : BaseEntity
    {
        [Required]
        public Guid ReligionId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string NameInOriginalLanguage { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public DateTime? FoundedDate { get; set; }

        [MaxLength(200)]
        public string FoundedBy { get; set; }

        [MaxLength(500)]
        public string MainCharacteristics { get; set; } 

        [MaxLength(500)]
        public string WikipediaUrl { get; set; }

        // Navigation Properties
        [ForeignKey("ReligionId")]
        public virtual Religion Religion { get; set; }
        public virtual ICollection<Person> Persons { get; set; }
    }

}
