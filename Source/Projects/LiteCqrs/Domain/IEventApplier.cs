using System.Collections.Generic;
using LiteCqrs.Eventing;

namespace LiteCqrs.Domain
{
	public interface IEventApplier
	{
		IEnumerable<IEvent> Apply<T>(T aggregateRoot, IEnumerable<IEvent> events) where T : IAggregateRoot;
	}
}