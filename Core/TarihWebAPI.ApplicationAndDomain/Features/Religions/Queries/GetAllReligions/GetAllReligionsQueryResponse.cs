namespace TarihWebAPI.ApplicationAndDomain.Features.Religions.Queries.GetAllReligions
{
    public class GetAllReligionsQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameInOriginalLanguage { get; set; }
        public string ShortName { get; set; }
        public string MainBranch { get; set; }
        public int? FoundedYear { get; set; }
        public string FoundedBy { get; set; }


        public string FoundedLocationName { get; set; }

        public long? ApproximateFollowers { get; set; }
        public string IconUrl { get; set; }
        public string ColorCode { get; set; }

  
        public int SectCount { get; set; }
    }
}