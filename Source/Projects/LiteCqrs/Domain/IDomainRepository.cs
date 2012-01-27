using System;

namespace LiteCqrs.Domain
{
	public interface IDomainRepository
	{
		void Store<T>(T aggregateRoot) where T : IAggregateRoot;
		T GetById<T>(Guid aggregateRootId) where T : IAggregateRoot;
	}
}