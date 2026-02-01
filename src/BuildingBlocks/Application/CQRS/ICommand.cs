using MediatR;

namespace BuildingBlocks.Application.CQRS;
public interface ICommand<out T> : IRequest<T> { }
