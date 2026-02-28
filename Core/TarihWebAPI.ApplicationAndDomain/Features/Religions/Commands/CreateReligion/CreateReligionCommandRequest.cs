using MediatR;

namespace TarihWebAPI.ApplicationAndDomain.Features.Religions.Commands.CreateReligion
{
    public class CreateReligionCommandRequest : IRequest<Guid>
    {
        public string Name { get; set; }
        public string NameInOriginalLanguage { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string MainBranch { get; set; }

        public int? FoundedYear { get; set; }

        public Guid? FounderPersonId { get; set; }
        public string FoundedBy { get; set; }

        public Guid? FoundedLocationId { get; set; }
        public string FoundedLocationName { get; set; }

        public long? ApproximateFollowers { get; set; }
        public string IconUrl { get; set; }
        public string ColorCode { get; set; }
        public string WikipediaUrl { get; set; }

        public List<CreateHolyBookDto> HolyBooks { get; set; } = new();

        public List<CreateHolyCityDto> HolyCities { get; set; } = new();
        public List<CreateSubBranchDto> SubBranches { get; set; } = new();
    }

    public class CreateHolyBookDto
    {
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public string WikipediaUrl { get; set; }
    }

    public class CreateHolyCityDto
    {
        public string CityName { get; set; }
        public string Significance { get; set; }
        public string Description { get; set; }
        public Guid? LocationId { get; set; }
    }

    public class CreateSubBranchDto
    {
        public string Name { get; set; }
        public string NameInOriginalLanguage { get; set; }
        public string Description { get; set; }
        public int? FoundedYear { get; set; }
        public string WikipediaUrl { get; set; }
    }
}