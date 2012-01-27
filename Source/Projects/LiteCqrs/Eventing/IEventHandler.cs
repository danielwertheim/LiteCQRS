using System;
using System.Reflection;

namespace LiteCqrs.Eventing
{
	/// <summary>
	/// Represents an event handler. That is a method that takes an <see cref="IEvent"/>
	/// as a single argument.
	/// </summary>
    public interface IEventHandler
    {
		/// <summary>
		/// The class holding the handler method.
		/// </summary>
		Type ContainerType { get; }
		/// <summary>
		/// The type of command this action handles. Should implement <see cref="IEvent"/>.
		/// </summary>
		Type EventType { get; }
		/// <summary>
		/// The method that will be invoked.
		/// </summary>
		MethodInfo Handler { get; }
    }
}