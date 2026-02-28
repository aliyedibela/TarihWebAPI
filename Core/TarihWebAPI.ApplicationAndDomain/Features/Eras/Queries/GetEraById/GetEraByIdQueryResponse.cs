namespace TarihWebAPI.ApplicationAndDomain.Features.Eras.Queries.GetEraById
{
    public class GetEraByIdQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameInOriginalLanguage { get; set; }
        public int StartYear { get; set; }
        public int? EndYear { get; set; }
        public string Description { get; set; }
        public string MainCharacteristics { get; set; }
        public string ColorCode { get; set; }
        public string WikipediaUrl { get; set; }

        public List<EraPeriodDto> Periods { get; set; } = new();

        public List<EraEventDto> Events { get; set; } = new();

        public List<EraPersonDto> Persons { get; set; } = new();
    }

    public class EraPeriodDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string StateName { get; set; }
        public int StartYear { get; set; }
        public int? EndYear { get; set; }
        public string ColorCode { get; set; }
    }

    public class EraEventDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public string EventType { get; set; }
        public int? Importance { get; set; }
    }

    public class EraPersonDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int? BirthYear { get; set; }
        public int? DeathYear { get; set; }
        public string ImageUrl { get; set; }
    }
}