using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarihWebAPI.ApplicationAndDomain.Entities
{
    [Table("Periods")]
    public class Period : BaseEntity
    {
        [Required]
        public Guid StateId { get; set; }  

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } 

        [MaxLength(200)]
        public string NameInOriginalLanguage { get; set; }

        [Required]
        public int StartYear { get; set; }

        public int? EndYear { get; set; }

        public Guid? EraId { get; set; }  

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [MaxLength(1000)]
        public string MainCharacteristics { get; set; } 

        [MaxLength(7)]
        public string ColorCode { get; set; }

        [MaxLength(500)]
        public string WikipediaUrl { get; set; }

        public int? OrderNumber { get; set; } 

        [ForeignKey("StateId")]
        public virtual State State { get; set; }

        [ForeignKey("EraId")]
        public virtual Era Era { get; set; }

        public virtual ICollection<HistoricalEvent> Events { get; set; }
        public virtual ICollection<Person> Persons { get; set; }
        public virtual ICollection<Treaty> Treaties { get; set; }
    }

}
