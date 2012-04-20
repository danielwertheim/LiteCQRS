using System;
using LiteCqrs.Commanding;
using LiteCqrs.Domain;
using LiteCqrs.TestScenario.Commands;
using LiteCqrs.TestScenario.Domain;

namespace LiteCqrs.TestScenario.SubCommandHandlers.SubA
{
    public class MySubCommandHandlers
    {
        private readonly IDomainRepository _domainRepository;

        [ThreadStatic]
        public static Action<ICommand> OnHandlerInvocation;

        public MySubCommandHandlers(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public void ThisCouldBeNamedWhatEver(MySubCommand cmd)
        {
            if(OnHandlerInvocation != null)
                OnHandlerInvocation.Invoke(cmd);

            var aggregate = new MyAggregate(cmd.AggregateId);
            aggregate.ActionRaisingSubEventWithApplier(cmd.Value);

            _domainRepository.Store(aggregate);
        }
    }
}