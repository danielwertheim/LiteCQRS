using System;
using LiteCqrs.Commanding;
using LiteCqrs.Domain;
using LiteCqrs.Domain.EventStores;
using LiteCqrs.Eventing;

namespace LiteCqrs
{
	public class CqrsRuntimeBuilder
	{
		public CommandHandlerContainerFactory CommandHandlerContainerFactory;
		public EventHandlerContainerFactory EventHandlerContainerFactory;

		public CqrsRuntimeBuilder()
		{
			CommandHandlerContainerFactory = Activator.CreateInstance;
			EventHandlerContainerFactory = Activator.CreateInstance;
		}

		public ICqrsRuntime Build(
			AssemblyScanConfig[] assembliesWithCommandHandlers,
			AssemblyScanConfig[] assembliesWithEventHandlers)
		{
			var commandHandlers = OnResolveCommandHandlers(assembliesWithCommandHandlers);
			var eventHandlers = OnResolveEventHandlers(assembliesWithEventHandlers);

			return new CqrsRuntime
			(
				OnCreateCommandBus(commandHandlers),
				OnCreateEventStore(),
				OnCreateEventApplier(),
				OnCreateEventPublisher(eventHandlers)
			);
		}

		protected virtual CommandHandlers OnResolveCommandHandlers(AssemblyScanConfig[] assembliesWithCommandHandlers)
		{
			var commandHandlers = new CommandHandlers();
			foreach (var assembly in assembliesWithCommandHandlers)
				commandHandlers.Register(new AssemblyCommandHandlerResolver(assembly).Resolve());

			return commandHandlers;
		}

		protected virtual ICommandBus OnCreateCommandBus(CommandHandlers commandHandlers)
		{
			return new InProcCommandBus(commandHandlers, new CommandHandlerInvoker(CommandHandlerContainerFactory));
		}

		protected virtual IEventStore OnCreateEventStore()
		{
			return new InMemoryEventStore();
		}

		protected virtual IEventApplier OnCreateEventApplier()
		{
			return new EventApplier();
		}

		protected virtual EventHandlers OnResolveEventHandlers(AssemblyScanConfig[] assembliesWithEventHandlers)
		{
			var eventHandlers = new EventHandlers();
			foreach (var assembly in assembliesWithEventHandlers)
				eventHandlers.Register(new AssemblyEventHandlerResolver(assembly).Resolve());

			return eventHandlers;
		}

		protected virtual IEventPublisher OnCreateEventPublisher(EventHandlers eventHandlers)
		{
			return new InProcEventPublisher(eventHandlers, new EventHandlerInvoker(EventHandlerContainerFactory));
		}
	}
}