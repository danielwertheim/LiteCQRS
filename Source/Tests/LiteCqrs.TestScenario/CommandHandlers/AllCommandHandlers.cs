using System;
using LiteCqrs.Commanding;
using LiteCqrs.Domain;
using LiteCqrs.TestScenario.Commands;
using LiteCqrs.TestScenario.Domain;

namespace LiteCqrs.TestScenario.CommandHandlers
{
    public class AllCommandHandlers
    {
        private readonly IDomainRepository _domainRepository;

        [ThreadStatic]
        public static Action<ICommand> OnHandlerInvocation;

        public AllCommandHandlers(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public void ThisCouldBeNamedWhatEver(MyCommand cmd)
        {
            if(OnHandlerInvocation != null)
                OnHandlerInvocation.Invoke(cmd);

            var aggregate = new MyAggregate(cmd.AggregateId);
            aggregate.ActionRaisingEventWithApplier(cmd.Value);

            _domainRepository.Store(aggregate);
        }
    }
}