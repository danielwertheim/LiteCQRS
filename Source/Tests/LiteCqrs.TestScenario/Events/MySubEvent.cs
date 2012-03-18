using System;
using LiteCqrs.Eventing;

namespace LiteCqrs.TestScenario.Events
{
    [Serializable]
    public class MySubEvent : IEvent
    {
        public Guid AggregateRootId { get; private set; }
        public string Value { get; private set; }
        
        public MySubEvent(Guid aggregateRootId, string value)
        {
            AggregateRootId = aggregateRootId;
            Value = value;
        }
    }
}