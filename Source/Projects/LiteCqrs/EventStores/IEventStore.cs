using System;
using System.Collections.Generic;
using LiteCqrs.Eventing;

namespace LiteCqrs.EventStores
{
	public interface IEventStore
	{
		void Store(Guid aggregateRootId, IEnumerable<IEvent> events);
		IEnumerable<IEvent> GetById(Guid aggregateRootId);
        IEnumerable<IEvent> GetByCommitId(Guid aggregateRootId, Guid commitId);
	}
}