namespace LiteCqrs.Commanding
{
	/// <summary>
	/// Invokes a command against a <see cref="ICommandHandler"/>
	/// </summary>
	public interface ICommandHandlerInvoker
	{
		/// <summary>
		/// Invokes the <see cref="ICommand"/> against the <see cref="ICommandHandler.Handler"/>
		/// in a class <see cref="ICommandHandler.ContainerType"/>.
		/// </summary>
		/// <param name="handler"></param>
		/// <param name="command"></param>
		void Invoke(ICommandHandler handler, ICommand command);
	}
}