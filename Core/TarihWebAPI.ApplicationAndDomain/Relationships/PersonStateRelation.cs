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
    [Table("PersonStateRelations")]
    public class PersonStateRelation : BaseEntity
    {
        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public Guid StateId { get; set; }

        [MaxLength(200)]
        public string Position { get; set; }  

        public int? OrderNumber { get; set; }  

        public int? StartYear { get; set; }

        public int? EndYear { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }


        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }
    }

}
