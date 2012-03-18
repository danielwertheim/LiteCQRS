using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using LiteCqrs.Eventing;
using NCore;

namespace LiteCqrs.EventStores
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly ConcurrentQueue<CommittedEvent> _store;

        public InMemoryEventStore()
        {
            _store = new ConcurrentQueue<CommittedEvent>();
        }

        public void Store(Guid aggregateRootId, IEnumerable<IEvent> events)
        {
            var commitId = Guid.NewGuid();
            var ts = SysDateTime.Now;

            foreach (var e in events)
                _store.Enqueue(new CommittedEvent { CommitId = commitId, TimeStamp = ts, Event = e });
        }

        public IEnumerable<IEvent> GetById(Guid aggregateRootId)
        {
            return _store.Where(e => e.AggregateRootId == aggregateRootId).Select(e => e.Event);
        }

        public IEnumerable<IEvent> GetByCommitId(Guid aggregateRootId, Guid commitId)
        {
            return _store.Where(ce => ce.CommitId == commitId && ce.AggregateRootId == aggregateRootId).Select(e => e.Event);
        }

        [Serializable]
        private class CommittedEvent
        {
            public Guid AggregateRootId
            {
                get { return Event.AggregateRootId; }
            }
            public Guid CommitId { get; set; }
            public DateTime TimeStamp { get; set; }
            public IEvent Event { get; set; }
        }
    }
}