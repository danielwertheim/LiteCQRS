using System;

namespace LiteCqrs.Eventing
{
    public interface IEvent
    {
    	Guid AggregateRootId { get; }
    }
}