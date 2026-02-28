using MediatR;

namespace TarihWebAPI.ApplicationAndDomain.Features.Eras.Queries.GetEraById
{
    public class GetEraByIdQueryRequest : IRequest<GetEraByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
}