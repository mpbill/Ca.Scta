using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ca.Scta.Dal.Connection;

namespace Ca.Scta.Dal.Cqrs.Base
{
    public interface ICommand
    {
        
    }

    public interface IQuery
    {
        
    }
    public interface ICommandHandler<in TCommand,TResult> where TCommand : ICommand
    {
        Task<TResult> HandleAsync(TCommand command);
    }

    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery
    {
        Task<TResult> HandleAsync(TQuery query);
    }

    public abstract class DapperCommandHandler<TCommand,TResult> : ICommandHandler<TCommand,TResult> where TCommand : ICommand
    {
        protected readonly IDbConnectionAsyncFactory Factory;

        protected DapperCommandHandler(IDbConnectionAsyncFactory factory)
        {
            Factory = factory;
        }

        public abstract Task<TResult> HandleAsync(TCommand command);
    }
    public abstract class EfCommandHandler<TCommand, TResult> : ICommandHandler<TCommand, TResult> where TCommand : ICommand
    {
        protected readonly Entities Context;

        protected EfCommandHandler(Entities context)
        {
            Context = context;
        }

        public abstract Task<TResult> HandleAsync(TCommand command);
    }
    public abstract class DapperQueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery
    {
        protected readonly IDbConnectionAsyncFactory Factory;

        protected DapperQueryHandler(IDbConnectionAsyncFactory factory)
        {
            Factory = factory;
        }

        public abstract Task<TResult> HandleAsync(TQuery query);
    }
    public abstract class EfQueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery
    {
        protected readonly Entities Context;

        protected EfQueryHandler(Entities context)
        {
            Context = context;
        }

        public abstract Task<TResult> HandleAsync(TQuery query);
    }
}
