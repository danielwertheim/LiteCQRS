using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using LiteCqrs.Eventing;

namespace LiteCqrs.Domain.EventStores
{
	public class InMemoryEventStore : IEventStore
	{
		private readonly ConcurrentQueue<IEvent> _store;

		public InMemoryEventStore()
		{
			_store = new ConcurrentQueue<IEvent>();
		}

		public IEnumerable<IEvent> Insert(IEnumerable<IEvent> events)
		{
			foreach (var e in events)
			{
				_store.Enqueue(e);
				yield return e;
			}
		}

		public IEnumerable<IEvent> GetById(Guid aggregateRootId)
		{
			return _store.Where(e => e.AggregateRootId == aggregateRootId);
		}
	}
}