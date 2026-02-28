using MediatR;
using TarihWebAPI.ApplicationAndDomain.Enums;

namespace TarihWebAPI.ApplicationAndDomain.Features.States.Commands.UpdateState
{
    public class UpdateStateCommandRequest : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OfficialName { get; set; }
        public string NameInOriginalLanguage { get; set; }
        public GovernmentType GovernmentType { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public Guid? CapitalLocationId { get; set; }
        public string CapitalName { get; set; }
        public Guid? OfficialReligionId { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string FlagUrl { get; set; }
        public string CoatOfArmsUrl { get; set; }
        public string ColorCode { get; set; }
        public string WikipediaUrl { get; set; }
    }
}