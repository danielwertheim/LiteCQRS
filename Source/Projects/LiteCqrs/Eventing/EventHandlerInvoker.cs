using EnsureThat;

namespace LiteCqrs.Eventing
{
	public class EventHandlerInvoker : IEventHandlerInvoker
	{
		protected readonly EventHandlerContainerFactory ContainerFactory;

		public EventHandlerInvoker(EventHandlerContainerFactory containerFactory)
		{
			Ensure.That(containerFactory, "ContainerFactory").IsNotNull();

			ContainerFactory = containerFactory;
		}

		public virtual void Invoke(IEventHandler handler, IEvent @event)
		{
			Ensure.That(handler, "handler").IsNotNull();
			Ensure.That(@event, "event").IsNotNull();

			var container = ContainerFactory.Invoke(handler.ContainerType);
			handler.Handler.Invoke(container, new object[] { @event });
		}
	}
}