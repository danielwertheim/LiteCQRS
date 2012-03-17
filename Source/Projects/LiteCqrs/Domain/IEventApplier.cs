using System.Collections.Generic;
using LiteCqrs.Eventing;

namespace LiteCqrs.Domain
{
    /// <summary>
    /// Used to apply events on an aggregate root.
    /// </summary>
	public interface IEventApplier
	{
		void Apply<T>(T aggregateRoot, IEnumerable<IEvent> events) where T : IAggregateRoot;
	}
}