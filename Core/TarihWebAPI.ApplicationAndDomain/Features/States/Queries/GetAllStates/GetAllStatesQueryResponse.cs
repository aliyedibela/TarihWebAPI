using TarihWebAPI.ApplicationAndDomain.Enums;

namespace TarihWebAPI.ApplicationAndDomain.Features.States.Queries.GetAllStates
{
    public class GetAllStatesQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OfficialName { get; set; }
        public string NameInOriginalLanguage { get; set; }
        public GovernmentType GovernmentType { get; set; }
        public string GovernmentTypeName => GovernmentType.ToString();

        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public string ColorCode { get; set; }
        public string FlagUrl { get; set; }
        public string CapitalName { get; set; }
        public string OfficialReligionName { get; set; }
        public List<string> OfficialLanguages { get; set; } = new();
    }
}