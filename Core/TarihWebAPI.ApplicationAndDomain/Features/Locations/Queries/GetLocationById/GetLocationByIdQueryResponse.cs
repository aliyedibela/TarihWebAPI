namespace TarihWebAPI.ApplicationAndDomain.Features.Locations.Queries.GetLocationById
{
    public class GetLocationByIdQueryResponse
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
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string WikipediaUrl { get; set; }
        public Guid? ParentLocationId { get; set; }
        public string ParentLocationName { get; set; }
        public List<SubLocationDto> SubLocations { get; set; } = new();
        public List<LocationEventDto> Events { get; set; } = new();
        public List<LocationPersonDto> BornPersons { get; set; } = new();
        public List<LocationPersonDto> DiedPersons { get; set; } = new();
    }

    public class SubLocationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class LocationEventDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public string EventType { get; set; }
        public int? Importance { get; set; }

    }

    public class LocationPersonDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int? BirthYear { get; set; }
        public int? DeathYear { get; set; }
        public string ImageUrl { get; set; }
    }
}