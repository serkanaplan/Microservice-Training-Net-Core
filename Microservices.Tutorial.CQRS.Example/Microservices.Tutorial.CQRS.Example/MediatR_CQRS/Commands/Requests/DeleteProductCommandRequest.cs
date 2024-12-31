using Microservices.Tutorial.CQRS.Example.MediatR_CQRS.Commands.Responses;
using MediatR;

namespace Microservices.Tutorial.CQRS.Example.MediatR_CQRS.Commands.Requests;

public class DeleteProductCommandRequest : IRequest<DeleteProductCommandResponse>
{
    public Guid ProductId { get; set; }
}
