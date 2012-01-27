using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using LiteCqrs.Eventing;

namespace LiteCqrs.Domain
{
	public class AggregateRoot : IAggregateRoot
	{
		private readonly ConcurrentQueue<IEvent> _queue = new ConcurrentQueue<IEvent>();

		protected Guid Id;

		protected void Raise(IEvent e)
		{
			_queue.Enqueue(e);
		}

		public IEnumerable<IEvent> DequeueEvents()
		{
			while (_queue.Count > 0)
			{
				IEvent e;
				if (_queue.TryDequeue(out e))
					yield return e;
			}
		}
	}
}