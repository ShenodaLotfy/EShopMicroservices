using MediatR;

namespace BuildingBlocks.CQRS
{
    // Not returning a response, Unit represents a void 
    public interface ICommand : IRequest<Unit>
    {
    }

    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
