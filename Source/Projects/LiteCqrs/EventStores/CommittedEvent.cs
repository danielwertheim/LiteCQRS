using System;
using LiteCqrs.Eventing;

namespace LiteCqrs.EventStores
{
    [Serializable]
    public class CommittedEvent : ICommittedEvent
    {
        public Guid Id
        {
            get { return AggregateRootId; }
        }

        public Guid AggregateRootId
        {
            get { return Event.AggregateRootId; }
        }

        public Guid CommitId { get; set; }
        public DateTime TimeStamp { get; set; }
        public IEvent Event { get; set; }
    }
}