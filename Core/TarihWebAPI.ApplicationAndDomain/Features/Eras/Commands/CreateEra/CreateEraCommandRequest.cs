using MediatR;

namespace TarihWebAPI.ApplicationAndDomain.Features.Eras.Commands.CreateEra
{
    public class CreateEraCommandRequest : IRequest<Guid>
    {
        public string Name { get; set; }
        public string NameInOriginalLanguage { get; set; }
        public int StartYear { get; set; }
        public int? EndYear { get; set; }
        public string Description { get; set; }
        public string MainCharacteristics { get; set; }
        public string ColorCode { get; set; }
        public string WikipediaUrl { get; set; }
    }
}