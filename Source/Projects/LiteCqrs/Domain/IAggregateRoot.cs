using System.Collections.Generic;
using LiteCqrs.Eventing;

namespace LiteCqrs.Domain
{
	public interface IAggregateRoot
	{
		IEnumerable<IEvent> DequeueEvents();
	}
}