using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarihWebAPI.ApplicationAndDomain.Entities;

namespace TarihWebAPI.ApplicationAndDomain.Features.TradeRoutes.Queries.GetAllTradeRoutes
{
    public class GetAllTradeRoutesQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AlternativeName { get; set; }
        public int StartYear { get; set; }
        public int? EndYear { get; set; }
        public string Description { get; set; }

        public string MainGoods { get; set; }

        public Guid? StartLocationId { get; set; }

        public Guid? EndLocationId { get; set; }

        public string RouteGeometryWkt { get; set; }

        [Range(1, 10)]
        public int? Importance { get; set; }

        [MaxLength(500)]
        public string MapUrl { get; set; }

        [MaxLength(500)]
        public string WikipediaUrl { get; set; }

        public virtual Location StartLocation { get; set; }

        public virtual Location EndLocation { get; set; }
    }
}
