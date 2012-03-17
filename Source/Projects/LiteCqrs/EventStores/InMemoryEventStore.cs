using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using LiteCqrs.Eventing;

namespace LiteCqrs.EventStores
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly ConcurrentQueue<ICommittedEvent> _store;

        public InMemoryEventStore()
        {
            _store = new ConcurrentQueue<ICommittedEvent>();
        }

        public void Insert(IEnumerable<IEvent> events)
        {
            var commitId = Guid.NewGuid();

            foreach (var e in events)
                _store.Enqueue(new CommittedEvent { CommitId = commitId, Event = e });
        }

        public IEnumerable<IEvent> GetById(Guid aggregateRootId)
        {
            return _store.Where(e => e.AggregateRootId == aggregateRootId);
        }

        public IEnumerable<IEvent> GetByCommitId(Guid commitId)
        {
            return _store.Where(ce => ce.CommitId == commitId);
        }
    }
}