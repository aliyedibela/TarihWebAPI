using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarihWebAPI.ApplicationAndDomain.Entities;

namespace TarihWebAPI.ApplicationAndDomain.Relationships
{
    [Table("PopulationData")]
    public class PopulationData : BaseEntity
    {
        [Required]
        public Guid StateId { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public long Population { get; set; }

        [MaxLength(500)]
        public string Source { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }
    }

}
