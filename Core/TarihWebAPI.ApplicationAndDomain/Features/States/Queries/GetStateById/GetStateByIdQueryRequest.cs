using MediatR;

namespace TarihWebAPI.ApplicationAndDomain.Features.States.Queries.GetStateById
{
    public class GetStateByIdQueryRequest : IRequest<GetStateByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
}