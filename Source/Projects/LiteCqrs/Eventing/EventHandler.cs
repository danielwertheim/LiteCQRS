using System;
using System.Reflection;

namespace LiteCqrs.Eventing
{
    public class EventHandler : IEventHandler
    {
        public Type ContainerType { get; private set; }
    	public Type EventType { get; private set; }
		public MethodInfo Handler { get; private set; }

		public EventHandler(Type containerType, Type eventType, MethodInfo handler)
    	{
    		ContainerType = containerType;
    		EventType = eventType;
    		Handler = handler;
    	}
    }
}