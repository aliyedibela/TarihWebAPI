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
    [Table("TreatyArticles")]
    public class TreatyArticle : BaseEntity
    {
        [Required]
        public Guid TreatyId { get; set; }

        [Required]
        public int ArticleNumber { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Content { get; set; }

        [MaxLength(500)]
        public string Summary { get; set; }

        [ForeignKey("TreatyId")]
        public virtual Treaty Treaty { get; set; }
    }

}
