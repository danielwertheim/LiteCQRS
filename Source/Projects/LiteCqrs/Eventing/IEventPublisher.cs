using System.Collections.Generic;

namespace LiteCqrs.Eventing
{
    public interface IEventPublisher
    {
        void Publish(IEnumerable<IEvent> events);
    }
}