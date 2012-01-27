using System.Collections.Generic;
using EnsureThat;

namespace LiteCqrs.Eventing
{
    public class InProcEventPublisher : IEventPublisher
    {
        protected readonly EventHandlers EventHandlers;
    	protected readonly IEventHandlerInvoker EventHandlerInvoker;

		public InProcEventPublisher(EventHandlers eventHandlers, IEventHandlerInvoker eventHandlerInvoker)
        {
			Ensure.That(eventHandlers, "eventHandlers").IsNotNull();
			Ensure.That(eventHandlerInvoker, "eventHandlerInvoker").IsNotNull();

        	EventHandlers = eventHandlers;
        	EventHandlerInvoker = eventHandlerInvoker;
        }

    	public virtual void Publish(IEnumerable<IEvent> events)
        {
            foreach (var e in events)
            {
                var handlers = EventHandlers.Get(e.GetType());
                foreach (var handler in handlers)
                   EventHandlerInvoker.Invoke(handler, e);
            }
        }
    }
}