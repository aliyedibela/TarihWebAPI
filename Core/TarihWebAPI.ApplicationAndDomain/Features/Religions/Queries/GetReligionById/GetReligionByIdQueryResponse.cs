namespace TarihWebAPI.ApplicationAndDomain.Features.Religions.Queries.GetReligionById
{
    public class GetReligionByIdQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameInOriginalLanguage { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string MainBranch { get; set; }

    
        public int? FoundedYear { get; set; }
        public Guid? FounderPersonId { get; set; }
        public string FounderPersonName { get; set; }  
        public Guid? FoundedLocationId { get; set; }
        public string FoundedLocationName { get; set; } 

        public long? ApproximateFollowers { get; set; }
        public string IconUrl { get; set; }
        public string ColorCode { get; set; }
        public string WikipediaUrl { get; set; }

        public List<ReligionHolyBookDto> HolyBooks { get; set; } = new();


        public List<ReligionHolyCityDto> HolyCities { get; set; } = new();


        public List<ReligionSubBranchDto> SubBranches { get; set; } = new();

   
        public List<ReligionSectDto> Sects { get; set; } = new();
    }

    public class ReligionHolyBookDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public string WikipediaUrl { get; set; }
    }

    public class ReligionHolyCityDto
    {
        public Guid Id { get; set; }
        public string CityName { get; set; }
        public string Significance { get; set; }
        public string Description { get; set; }
        public Guid? LocationId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }

    public class ReligionSubBranchDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameInOriginalLanguage { get; set; }
        public string Description { get; set; }
        public int? FoundedYear { get; set; }
        public string WikipediaUrl { get; set; }
    }

    public class ReligionSectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameInOriginalLanguage { get; set; }
        public string Description { get; set; }
        public string FoundedBy { get; set; }
        public string WikipediaUrl { get; set; }
    }
}