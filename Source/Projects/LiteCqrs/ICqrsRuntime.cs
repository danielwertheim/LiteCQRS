using LiteCqrs.Commanding;
using LiteCqrs.Domain;
using LiteCqrs.Eventing;

namespace LiteCqrs
{
	public interface ICqrsRuntime
	{
		ICommandBus CommandBus { get; }
		IEventStore EventStore { get; }
		IEventApplier EventApplier { get; }
		IEventPublisher EventPublisher { get; }
		IDomainRepository GetDomainRepository();
	}
}