using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EnsureThat;

namespace LiteCqrs.Eventing
{
	/// <summary>
	/// Scans a certain <see cref="Assembly"/> for <see cref="IEventHandler"/> instances.
	/// </summary>
	public class AssemblyEventHandlerResolver : IEventHandlerResolver
	{
		protected readonly Type EventMarkerType;
		protected readonly Assembly Assembly;
		protected readonly Func<string, bool> NamespaceFilter;

		public AssemblyEventHandlerResolver(AssemblyScanConfig config)
		{
			Ensure.That(config, "config").IsNotNull();
			
			EventMarkerType = typeof (IEvent);
			Assembly = config.Assembly;
			NamespaceFilter = config.NamespaceFilter ?? (n => n != null && n.EndsWith("EventHandlers"));
		}

		public virtual IEnumerable<IEventHandler> Resolve()
		{
			var handlerContainers = Assembly.GetTypes().Where(TypeIsEligibleHandlerContainer)
				.Select(t => new
				{
					HandlerContainerType = t,
					Handlers = t.GetMethods().Where(MethodIsAlligableHandler)
				})
				.Where(hr => hr.Handlers.Any()).ToArray();

			foreach (var handlerContainer in handlerContainers)
			{
				var containerType = handlerContainer.HandlerContainerType;

				foreach (var handler in handlerContainer.Handlers)
				{
					var commandType = handler.GetParameters()[0].ParameterType;

					yield return new EventHandler(containerType, commandType, handler);
				}
			}
		}

		protected virtual bool TypeIsEligibleHandlerContainer(Type t)
		{
			return t.IsPublic && t.IsClass && !t.IsAbstract && NamespaceFilter(t.Namespace);
		}

		protected virtual bool MethodIsAlligableHandler(MethodInfo m)
		{
			var isOk = (m.IsPublic && !m.IsAbstract && !m.IsConstructor && !m.IsGenericMethod && !m.IsGenericMethodDefinition);
			if (!isOk)
				return false;

			var p = m.GetParameters();

			return p.Length == 1 && EventMarkerType.IsAssignableFrom(p[0].ParameterType);
		}
	}
}