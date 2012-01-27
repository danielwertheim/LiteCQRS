namespace LiteCqrs.Eventing
{
	/// <summary>
	/// Invokes a command against a <see cref="IEventHandler"/>
	/// </summary>
	public interface IEventHandlerInvoker
	{
		/// <summary>
		/// Invokes the <see cref="IEvent"/> against the <see cref="IEventHandler.Handler"/>
		/// in a class <see cref="IEventHandler.ContainerType"/>.
		/// </summary>
		/// <param name="handler"></param>
		/// <param name="event"> </param>
		void Invoke(IEventHandler handler, IEvent @event);
	}
}