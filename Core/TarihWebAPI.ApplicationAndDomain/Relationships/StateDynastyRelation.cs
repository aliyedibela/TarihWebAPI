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
    [Table("StateDynastyRelations")]
    public class StateDynastyRelation : BaseEntity
    {
        [Required]
        public Guid StateId { get; set; }

        [Required]
        public Guid DynastyId { get; set; }

        public int? StartYear { get; set; }

        public int? EndYear { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }

        [ForeignKey("DynastyId")]
        public virtual Dynasty Dynasty { get; set; }
    }

}
