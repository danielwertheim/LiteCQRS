using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using LiteCqrs.Eventing;

namespace LiteCqrs.Domain
{
	public class AggregateRoot : IAggregateRoot
	{
        protected readonly ConcurrentQueue<IEvent> Queue = new ConcurrentQueue<IEvent>();

		protected Guid Id;

		protected virtual void Raise(IEvent e)
		{
			Queue.Enqueue(e);
		}

		public virtual IEnumerable<IEvent> GetUncommittedEvents()
		{
            while (Queue.Count > 0)
			{
				IEvent e;
				if (Queue.TryDequeue(out e))
					yield return e;
			}
		}
	}
}