using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using EnsureThat;

namespace LiteCqrs.Eventing
{
    public class EventHandlers
    {
    	protected readonly ConcurrentDictionary<Type, IList<IEventHandler>> Registrations;

        public EventHandlers()
        {
            Registrations = new ConcurrentDictionary<Type, IList<IEventHandler>>();
        }

        public virtual void Clear()
        {
            Registrations.Clear();
        }

		public virtual void Register(IEnumerable<IEventHandler> handlers)
		{
			Ensure.That(handlers, "handlers").IsNotNull();

            foreach (var handler in handlers)
            {
                Registrations.AddOrUpdate(handler.EventType, new List<IEventHandler> { handler },
                    (t, v) =>
                    {
                        v.Add(handler);
                        return v;
                    });
            }
        }

		public IEnumerable<IEventHandler> Get(Type eventType)
		{
			return Registrations[eventType];
		}
    }
}