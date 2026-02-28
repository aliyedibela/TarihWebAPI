using MediatR;

namespace TarihWebAPI.ApplicationAndDomain.Features.Locations.Commands.UpdateLocation
{
    public class UpdateLocationCommandRequest : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameInOriginalLanguage { get; set; }
        public string Type { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ModernName { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public Guid? ParentLocationId { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string WikipediaUrl { get; set; }
    }
}