using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EnsureThat;

namespace LiteCqrs.Commanding
{
	/// <summary>
	/// Scans a certain <see cref="Assembly"/> for <see cref="ICommandHandler"/> instances.
	/// </summary>
	public class AssemblyCommandHandlerResolver : ICommandHandlerResolver
	{
		protected readonly Type CommandMarkerType;
		protected readonly Assembly Assembly;
		protected readonly Func<string, bool> NamespaceFilter;

		public AssemblyCommandHandlerResolver(AssemblyScanConfig config)
		{
			Ensure.That(config, "config").IsNotNull();

			CommandMarkerType = typeof (ICommand);
			Assembly = config.Assembly;
			NamespaceFilter = config.NamespaceFilter ?? (n => n != null && n.EndsWith("CommandHandlers"));
		}

		public virtual IEnumerable<ICommandHandler> Resolve()
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

					yield return new CommandHandler(containerType, commandType, handler);
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

			return p.Length == 1 && CommandMarkerType.IsAssignableFrom(p[0].ParameterType);
		}
	}
}