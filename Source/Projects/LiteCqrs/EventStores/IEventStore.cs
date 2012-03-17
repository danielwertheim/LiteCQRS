using System;
using System.Collections.Generic;
using LiteCqrs.Eventing;

namespace LiteCqrs.EventStores
{
	public interface IEventStore
	{
		void Store(IEnumerable<IEvent> events);
		IEnumerable<IEvent> GetById(Guid aggregateRootId);
        IEnumerable<IEvent> GetByCommitId(Guid commitId);
	}
}