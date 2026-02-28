using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Enums;

namespace TarihWebAPI.ApplicationAndDomain.Relationships
{
    [Table("PersonRelations")]
    public class PersonRelation : BaseEntity
    {
        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public Guid RelatedPersonId { get; set; }

        [Required]
        public PersonRelationType RelationType { get; set; }

        // İlişkinin aktif olduğu dönem (opsiyonel)
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }

        [Column(TypeName = "text")]
        public string Notes { get; set; }

        // Navigation Properties
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        [ForeignKey("RelatedPersonId")]
        public virtual Person RelatedPerson { get; set; }
    }
}