using MediatR;

namespace BuildingBlocks.Application.CQRS;
public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>;