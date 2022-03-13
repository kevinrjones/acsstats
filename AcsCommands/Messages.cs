using AcsCommandHandlers;
using AcsTypes.Error;
using CSharpFunctionalExtensions;

// ReSharper disable InconsistentNaming

namespace AcsCommands
{
  public sealed class Messages
  {
    private readonly IServiceProvider _provider;

    public Messages(IServiceProvider provider)
    {
      _provider = provider ?? throw new Exception();
    }

    public Result Dispatch(ICommand command)
    {
      Type type = typeof(ICommandHandler<>);
      Type[] typeArgs = { command.GetType() };
      Type handlerType = type.MakeGenericType(typeArgs);

      dynamic handler = _provider.GetService(handlerType) ?? throw new Exception("Cannot find handler");
      Result result = handler.Handle((dynamic)command);

      return result;
    }

    public async Task<Result<T, Error>> Dispatch<T>(IQuery<T> query)
    {
      Type type = typeof(IQueryHandler<,>);
      Type[] typeArgs = { query.GetType(), typeof(T) };
      Type handlerType = type.MakeGenericType(typeArgs);

      dynamic handler = _provider.GetService(handlerType) ?? throw new Exception("Cannot find handler");
      Result<T, Error> result = await handler.Handle((dynamic)query);

      return result;
    }
  }
}