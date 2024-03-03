using Application.Exceptions;
using Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class UseCaseExecutor
    {
        private readonly IApplicationActor actor;

        public UseCaseExecutor(IApplicationActor actor)
        {
            this.actor = actor;
        }

        public TResult ExecuteQuery<TSearch, TResult>(IQuery<TSearch, TResult> query, TSearch search)
        {
            if (!actor.AllowedUseCases.Contains(query.Id))
            {
                throw new UnauthorizedUseCaseException(query, actor);
            }

            return query.Execute(search);
        }

        public void ExecuteCommand<TRequest>(ICommand<TRequest> command, TRequest request)
        {
            if (!actor.AllowedUseCases.Contains(command.Id))
            {
                throw new UnauthorizedUseCaseException(command, actor);
            }

            command.Execute(request);
        }
    }
}
