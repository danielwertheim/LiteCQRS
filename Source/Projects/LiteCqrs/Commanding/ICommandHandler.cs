using System;
using System.Reflection;

namespace LiteCqrs.Commanding
{
	/// <summary>
	/// Represents a command handler. That is a method that takes an <see cref="ICommand"/>
	/// as a single argument.
	/// </summary>
    public interface ICommandHandler
    {
		/// <summary>
		/// The class holding the handler method.
		/// </summary>
    	Type ContainerType { get; }
		/// <summary>
		/// The type of command this action handles. Should implement <see cref="ICommand"/>.
		/// </summary>
    	Type CommandType { get; }
		/// <summary>
		/// The method that will be invoked.
		/// </summary>
    	MethodInfo Handler { get; }
    }
}