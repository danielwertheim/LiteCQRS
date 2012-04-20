using System;
using LiteCqrs.Commanding;
using LiteCqrs.Domain;
using LiteCqrs.TestScenario.Commands;
using LiteCqrs.TestScenario.Domain;

namespace LiteCqrs.TestScenario.CommandHandlers
{
    public class MyCommandHandlers
    {
        private readonly IDomainRepository _domainRepository;

        [ThreadStatic]
        public static Action<ICommand> OnHandlerInvocation;

        public MyCommandHandlers(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public void ThisHandlerMethodCouldBeNamedWhatEver(MyCommand cmd)
        {
            if(OnHandlerInvocation != null)
                OnHandlerInvocation.Invoke(cmd);

            var aggregate = new MyAggregate(cmd.AggregateId);
            aggregate.ActionRaisingEventWithApplier(cmd.Value);

            _domainRepository.Store(aggregate);
        }
    }
}