using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarihWebAPI.ApplicationAndDomain.Entities
{
    [Table("Eras")]
    public class Era : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }  

        [MaxLength(200)]
        public string NameInOriginalLanguage { get; set; }

        [Required]
        public int StartYear { get; set; }

        public int? EndYear { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [MaxLength(1000)]
        public string MainCharacteristics { get; set; } 

        [MaxLength(7)]
        public string ColorCode { get; set; }

        [MaxLength(500)]
        public string WikipediaUrl { get; set; }

        public virtual ICollection<Period> Periods { get; set; }
        public virtual ICollection<HistoricalEvent> Events { get; set; }
        public virtual ICollection<Person> Persons { get; set; }
        public virtual ICollection<Treaty> Treaties { get; set; }
    }

}
