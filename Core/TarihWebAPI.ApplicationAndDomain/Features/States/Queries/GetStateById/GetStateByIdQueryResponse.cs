using TarihWebAPI.ApplicationAndDomain.Enums;

namespace TarihWebAPI.ApplicationAndDomain.Features.States.Queries.GetStateById
{
    // Detay sayfası için tam response
    public class GetStateByIdQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OfficialName { get; set; }
        public string NameInOriginalLanguage { get; set; }
        public GovernmentType GovernmentType { get; set; }
        public string GovernmentTypeName => GovernmentType.ToString();
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }

        // Başkent
        public Guid? CapitalLocationId { get; set; }
        public string CapitalName { get; set; }

        // Din
        public Guid? OfficialReligionId { get; set; }
        public string OfficialReligionName { get; set; }

        // Görsel / meta
        public string Currency { get; set; }
        public string Description { get; set; }
        public string FlagUrl { get; set; }
        public string CoatOfArmsUrl { get; set; }
        public string ColorCode { get; set; }
        public string WikipediaUrl { get; set; }

        // İlişkili listeler
        public List<StateLanguageDto> OfficialLanguages { get; set; } = new();
        public List<StatePeriodDto> Periods { get; set; } = new();
        public List<StateDynastyDto> Dynasties { get; set; } = new();
        public List<StatePopulationDto> PopulationData { get; set; } = new();
    }

    public class StateLanguageDto
    {
        public string Language { get; set; }
        public bool IsPrimary { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
    }

    public class StatePeriodDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StartYear { get; set; }
        public int? EndYear { get; set; }
        public string ColorCode { get; set; }
        public int? OrderNumber { get; set; }
    }

    public class StateDynastyDto
    {
        public Guid DynastyId { get; set; }
        public string DynastyName { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
    }

    public class StatePopulationDto
    {
        public int Year { get; set; }
        public long Population { get; set; }
        public string Source { get; set; }
    }
}