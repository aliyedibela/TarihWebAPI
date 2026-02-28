using MediatR;

namespace TarihWebAPI.ApplicationAndDomain.Features.States.Queries.GetAllStates
{
    public class GetAllStatesQueryRequest : IRequest<IList<GetAllStatesQueryResponse>>
    {

        public int? Year { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}