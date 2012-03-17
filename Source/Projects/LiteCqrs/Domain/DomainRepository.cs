using System;
using System.Collections.Generic;
using System.Linq;
using LiteCqrs.EventStores;
using LiteCqrs.Eventing;

namespace LiteCqrs.Domain
{
	public class DomainRepository : IDomainRepository
	{
		protected readonly IEventStore EventStore;
		protected readonly IEventApplier EventApplier;
		protected readonly IEventPublisher EventPublisher;

		public DomainRepository(IEventStore eventStore, IEventApplier eventApplier,  IEventPublisher eventPublisher)
		{
			EventStore = eventStore;
			EventApplier = eventApplier;
			EventPublisher = eventPublisher;
		}

		public void Store<T>(T aggregateRoot) where T : IAggregateRoot
		{
		    var aggregateRootEvents = aggregateRoot.DequeueEvents().ToArray();
			StoreEvents(aggregateRootEvents);
            ApplyEvents(aggregateRoot, aggregateRootEvents);
            PublishEvents(aggregateRootEvents);
		}

		public T GetById<T>(Guid aggregateRootId) where T : IAggregateRoot
		{
            var storedEvents = GetEventsById(aggregateRootId).ToArray();
			var aggregateRoot = (T)Activator.CreateInstance(typeof(T), true);
			ApplyEvents(aggregateRoot, storedEvents);

			return aggregateRoot;
		}

		protected virtual void StoreEvents(IEnumerable<IEvent> events)
		{
			EventStore.Store(events);
		}

		protected virtual void ApplyEvents<T>(T aggregateRoot, IEnumerable<IEvent> events) where T : IAggregateRoot
		{
			EventApplier.Apply(aggregateRoot, events);
		}

		protected virtual void PublishEvents(IEnumerable<IEvent> events)
		{
			EventPublisher.Publish(events);
		}

		protected virtual IEnumerable<IEvent> GetEventsById(Guid aggregateRootId)
		{
			return EventStore.GetById(aggregateRootId);
		}
	}
}