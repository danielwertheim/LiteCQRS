using System;
using LiteCqrs.Commanding;
using LiteCqrs.Domain;
using LiteCqrs.EventStores;
using LiteCqrs.Eventing;

namespace LiteCqrs
{
	public class CqrsRuntimeBuilder
	{
        public CommandHandlerContainerFactory CommandHandlerContainerFactory { get; set; }
        public EventHandlerContainerFactory EventHandlerContainerFactory { get; set; }
	    public Func<CommandHandlers, ICommandBus> CommandBusFactory { get; set; }
	    public Func<EventHandlers, IEventPublisher> EventPublisherFactory { get; set; }
        public Func<IEventStore> EventStoreFactory { get; set; }
        public Func<IEventApplier> EventApplierFactory { get; set; }
 
	    public CqrsRuntimeBuilder()
		{
			CommandHandlerContainerFactory = Activator.CreateInstance;
			EventHandlerContainerFactory = Activator.CreateInstance;
            CommandBusFactory = commandHandlers => new InProcCommandBus(commandHandlers, new CommandHandlerInvoker(CommandHandlerContainerFactory));
            EventPublisherFactory = eventHandlers => new InProcEventPublisher(eventHandlers, new EventHandlerInvoker(EventHandlerContainerFactory));
            EventStoreFactory = () => new InMemoryEventStore();
            EventApplierFactory = () => new EventApplier();
		}

		public ICqrsRuntime Build(
			AssemblyScanConfig[] assembliesWithCommandHandlers,
			AssemblyScanConfig[] assembliesWithEventHandlers)
		{
			var commandHandlers = OnResolveCommandHandlers(assembliesWithCommandHandlers);
			var eventHandlers = OnResolveEventHandlers(assembliesWithEventHandlers);

			return new CqrsRuntime
			(
				CommandBusFactory.Invoke(commandHandlers),
				EventStoreFactory.Invoke(),
				EventApplierFactory.Invoke(),
                EventPublisherFactory.Invoke(eventHandlers)
			);
		}

		protected virtual CommandHandlers OnResolveCommandHandlers(AssemblyScanConfig[] assembliesWithCommandHandlers)
		{
			var commandHandlers = new CommandHandlers();
			foreach (var assembly in assembliesWithCommandHandlers)
				commandHandlers.Register(new AssemblyCommandHandlerResolver(assembly).Resolve());

			return commandHandlers;
		}

		protected virtual EventHandlers OnResolveEventHandlers(AssemblyScanConfig[] assembliesWithEventHandlers)
		{
			var eventHandlers = new EventHandlers();
			foreach (var assembly in assembliesWithEventHandlers)
				eventHandlers.Register(new AssemblyEventHandlerResolver(assembly).Resolve());

			return eventHandlers;
		}
	}
}