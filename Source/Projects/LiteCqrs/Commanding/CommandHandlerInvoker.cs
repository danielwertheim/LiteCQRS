using EnsureThat;

namespace LiteCqrs.Commanding
{
	public class CommandHandlerInvoker : ICommandHandlerInvoker
	{
		protected readonly CommandHandlerContainerFactory ContainerFactory;

		public CommandHandlerInvoker(CommandHandlerContainerFactory containerFactory)
		{
			Ensure.That(containerFactory, "ContainerFactory").IsNotNull();

			ContainerFactory = containerFactory;
		}

		public virtual void Invoke(ICommandHandler handler, ICommand command)
		{
			Ensure.That(handler, "handler").IsNotNull();
			Ensure.That(command, "command").IsNotNull();

			var container = ContainerFactory.Invoke(handler.ContainerType);
			handler.Handler.Invoke(container, new object[] { command });
		}
	}
}