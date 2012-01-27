using EnsureThat;
using LiteCqrs.Commanding;
using LiteCqrs.Domain;
using LiteCqrs.Eventing;

namespace LiteCqrs
{
	public class CqrsRuntime : ICqrsRuntime
	{
		public ICommandBus CommandBus { get; private set; }
		public IEventStore EventStore { get; private set; }
		public IEventApplier EventApplier { get; private set; }
		public IEventPublisher EventPublisher { get; private set; }

		public CqrsRuntime(ICommandBus commandBus, IEventStore eventStore, IEventApplier eventApplier, IEventPublisher eventPublisher)
		{
			Ensure.That(commandBus, "commandBus").IsNotNull();
			Ensure.That(eventStore, "eventStore").IsNotNull();
			Ensure.That(eventApplier, "eventApplier").IsNotNull();
			Ensure.That(eventPublisher, "eventPublisher").IsNotNull();

			CommandBus = commandBus;
			EventStore = eventStore;
			EventApplier = eventApplier;
			EventPublisher = eventPublisher;
		}

		public IDomainRepository GetDomainRepository()
		{
			return new DomainRepository(EventStore, EventApplier, EventPublisher);
		}
	}
}