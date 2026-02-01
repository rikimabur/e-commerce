using MediatR;

namespace BuildingBlocks.Application.CQRS;
public interface IQuery<T> : IRequest<T> { }