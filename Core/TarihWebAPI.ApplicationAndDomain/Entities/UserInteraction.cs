using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarihWebAPI.ApplicationAndDomain.Enums;

namespace TarihWebAPI.ApplicationAndDomain.Entities
{
    [Table("UserInteractions")]
    public class UserInteraction : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public EntityTypeEnum EntityType { get; set; }

        [Required]
        public Guid EntityId { get; set; }

        public InteractionType InteractionType { get; set; }

        [Column(TypeName = "text")]
        public string Notes { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }

}
