using System;

namespace LiteCqrs.Commanding
{
	/// <summary>
	/// Creates an instance of a class containing a an <see cref="ICommandHandler"/>.
	/// That is, a class that has a public method, accepting an <see cref="ICommand"/>
	/// as a single argument.
	/// </summary>
	/// <param name="type"></param>
	/// <returns></returns>
	public delegate object CommandHandlerContainerFactory(Type type);
}