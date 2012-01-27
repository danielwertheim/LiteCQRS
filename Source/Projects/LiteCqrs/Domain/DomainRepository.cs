using System;
using System.Collections.Generic;
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
			var storedEvents = StoreEvents(aggregateRoot.DequeueEvents());
			var appliedEvents = ApplyEvents(aggregateRoot, storedEvents);
			PublishEvents(appliedEvents);
		}

		public T GetById<T>(Guid aggregateRootId) where T : IAggregateRoot
		{
			var aggregateRoot = (T)Activator.CreateInstance(typeof(T), true);
			var storedEvents = GetEventsById(aggregateRootId);
			ApplyEvents(aggregateRoot, storedEvents);

			return aggregateRoot;
		}

		protected virtual IEnumerable<IEvent> StoreEvents(IEnumerable<IEvent> events)
		{
			return EventStore.Insert(events);
		}

		protected virtual IEnumerable<IEvent> ApplyEvents<T>(T aggregateRoot, IEnumerable<IEvent> events) where T : IAggregateRoot
		{
			return EventApplier.Apply(aggregateRoot, events);
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