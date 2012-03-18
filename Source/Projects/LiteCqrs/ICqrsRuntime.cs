using LiteCqrs.Commanding;
using LiteCqrs.Domain;
using LiteCqrs.EventStores;
using LiteCqrs.Eventing;

namespace LiteCqrs
{
	public interface ICqrsRuntime
	{
        ICommandBus CommandBus { get; set; }
        IEventStore EventStore { get; set; }
        IEventApplier EventApplier { get; set; }
        IEventPublisher EventPublisher { get; set; }
		IDomainRepository GetDomainRepository();
	}
}