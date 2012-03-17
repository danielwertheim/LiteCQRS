using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LiteCqrs.Eventing;

namespace LiteCqrs.Domain
{
	public class EventApplier : IEventApplier
	{
		protected const BindingFlags EventApplierBindingFlags =
			BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.Instance;

		public virtual void Apply<T>(T aggregateRoot, IEnumerable<IEvent> events) where T : IAggregateRoot
		{
			var eventType = typeof(IEvent);
			var aggregateRootType = aggregateRoot.GetType();
			var eventAppliers = aggregateRootType.GetMethods(EventApplierBindingFlags)
				.Where(m => m.Name.StartsWith("on", StringComparison.OrdinalIgnoreCase))
				.Select(m => new
				{
					Method = m,
					Param = m.GetParameters().SingleOrDefault(p => eventType.IsAssignableFrom(p.ParameterType))
				})
				.Where(m => m.Param != null)
				.ToDictionary(i => i.Param.ParameterType, i => i.Method);

			foreach (var e in events)
			{
				MethodInfo eventApplier;
				if (eventAppliers.TryGetValue(e.GetType(), out eventApplier))
					eventApplier.Invoke(aggregateRoot, new object[] { e });
			}
		}
	}
}