using AcsCommands;
using AcsTypes.Error;
using CSharpFunctionalExtensions;

namespace AcsCommandHandlers;


public interface ICommandHandler<TCommand>
    where TCommand : ICommand
{
    Result Handle(TCommand command);
}

public interface IQueryHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    Task<Result<TResult, Error>> Handle(TQuery query);
}

