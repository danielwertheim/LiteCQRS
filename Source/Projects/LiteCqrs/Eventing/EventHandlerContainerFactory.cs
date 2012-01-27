using System;

namespace LiteCqrs.Eventing
{
	/// <summary>
	/// Creates an instance of a class containing a an <see cref="IEventHandler"/>.
	/// That is, a class that has a public method, accepting an <see cref="IEvent"/>.
	/// as a single argument.
	/// </summary>
	/// <param name="type"></param>
	/// <returns></returns>
	public delegate object EventHandlerContainerFactory(Type type);
}