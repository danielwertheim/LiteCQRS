using System;
using System.Collections.Generic;
using LiteCqrs.Eventing;

namespace LiteCqrs.Domain
{
	public interface IEventStore
	{
		IEnumerable<IEvent> Insert(IEnumerable<IEvent> events);
		IEnumerable<IEvent> GetById(Guid aggregateRootId);
	}
}