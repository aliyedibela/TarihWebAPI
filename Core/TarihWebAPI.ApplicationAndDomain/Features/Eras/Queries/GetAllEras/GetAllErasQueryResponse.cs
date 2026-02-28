namespace TarihWebAPI.ApplicationAndDomain.Features.Eras.Queries.GetAllEras
{
    public class GetAllErasQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameInOriginalLanguage { get; set; }
        public int StartYear { get; set; }
        public int? EndYear { get; set; }
        public string MainCharacteristics { get; set; }
        public string ColorCode { get; set; }
        public string WikipediaUrl { get; set; }


        public int PeriodCount { get; set; }

        public int EventCount { get; set; }
    }
}