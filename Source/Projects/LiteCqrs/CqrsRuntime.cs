using EnsureThat;
using LiteCqrs.Commanding;
using LiteCqrs.Domain;
using LiteCqrs.EventStores;
using LiteCqrs.Eventing;

namespace LiteCqrs
{
	public class CqrsRuntime : ICqrsRuntime
	{
	    private ICommandBus _commandBus;
        private IEventStore _eventStore;
        private IEventApplier _eventApplier;
        private IEventPublisher _eventPublisher;

        public ICommandBus CommandBus
	    {
	        get
	        {
	            return _commandBus;
	        }
	        set
	        {
	            Ensure.That(value, "CommandBus").IsNotNull();
	            _commandBus = value;
	        }
	    }
        
	    public IEventStore EventStore
	    {
	        get
	        {
	            return _eventStore;
	        }
	        set
	        {
                Ensure.That(value, "EventStore").IsNotNull();
                _eventStore = value;
	        }
	    }

	    public IEventApplier EventApplier
	    {
	        get
	        {
	            return _eventApplier;
	        }
	        set
	        {
                Ensure.That(value, "EventApplier").IsNotNull();
	            _eventApplier = value;
	        }
	    }

	    public IEventPublisher EventPublisher
	    {
	        get
	        {
	            return _eventPublisher;
	        }
	        set
	        {
                Ensure.That(value, "EventPublisher").IsNotNull();
	            _eventPublisher = value;
	        }
	    }

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