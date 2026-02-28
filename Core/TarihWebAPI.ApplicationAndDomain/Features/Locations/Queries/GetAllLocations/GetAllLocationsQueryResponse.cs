namespace TarihWebAPI.ApplicationAndDomain.Features.Locations.Queries.GetAllLocations
{
    public class GetAllLocationsQueryResponse
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
        public string ParentLocationName { get; set; }
        public int SubLocationCount { get; set; }
    }
}