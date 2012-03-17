using System;
using LiteCqrs.Eventing;

namespace LiteCqrs.EventStores
{
    public interface ICommittedEvent : IEvent
    {
        Guid Id { get; }
        Guid CommitId { get; set; }
        DateTime TimeStamp { get; set; }
        IEvent Event { get; set; }
    }
}