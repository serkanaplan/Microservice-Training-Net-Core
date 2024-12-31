using Microservices.Tutorial.CQRS.Example.MediatR_CQRS.Queries.Responses;
using MediatR;

namespace Microservices.Tutorial.CQRS.Example.MediatR_CQRS.Queries.Requests;

public class GetAllProductQueryRequest : IRequest<List<GetAllProductQueryResponse>>
{
}
