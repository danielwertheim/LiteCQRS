using System;
using LiteCqrs.Eventing;

namespace LiteCqrs.TestScenario.Events
{
    [Serializable]
    public class MyAggregateCreatedEvent : IEvent
    {
        public Guid AggregateRootId { get; private set; }

        public MyAggregateCreatedEvent(Guid aggregateRootId)
        {
            AggregateRootId = aggregateRootId;
        }
    }
}