using EnsureThat;

namespace LiteCqrs.Commanding
{
    public class InProcCommandBus : ICommandBus
    {
        protected readonly CommandHandlers CommandHandlers;
    	protected readonly ICommandHandlerInvoker CommandHandlerInvoker;

		public InProcCommandBus(CommandHandlers commandHandlers, ICommandHandlerInvoker commandHandlerInvoker)
		{
			Ensure.That(commandHandlers, "commandHandlers").IsNotNull();
			Ensure.That(commandHandlerInvoker, "commandHandlerInvoker").IsNotNull();

        	CommandHandlers = commandHandlers;
        	CommandHandlerInvoker = commandHandlerInvoker;
        }

    	public virtual void Send<T>(T command) where T : ICommand
        {
        	CommandHandlerInvoker.Invoke(CommandHandlers.Get(typeof(T)), command);
        }
    }
}