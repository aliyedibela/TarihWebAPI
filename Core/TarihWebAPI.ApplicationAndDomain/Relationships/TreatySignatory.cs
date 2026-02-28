using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TarihWebAPI.ApplicationAndDomain.Entities;

namespace TarihWebAPI.ApplicationAndDomain.Relationships
{
    [Table("TreatySignatories")]
    public class TreatySignatory : BaseEntity
    {
        [Required]
        public Guid TreatyId { get; set; }

        public Guid? StateId { get; set; }

        public Guid? SignatoryPersonId { get; set; }

        [MaxLength(200)]
        public string SignatoryTitle { get; set; }

        [Column(TypeName = "text")]
        public string Notes { get; set; }

        [ForeignKey("TreatyId")]
        public virtual Treaty Treaty { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }

        [ForeignKey("SignatoryPersonId")]
        public virtual Person SignatoryPerson { get; set; }
    }
}