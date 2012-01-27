using System.Collections.Generic;

namespace LiteCqrs.Eventing
{
	/// <summary>
	/// Responsible for resolving and creating all availible
	/// <see cref="IEventHandler"/> in the domain.
	/// </summary>
    public interface IEventHandlerResolver
    {
        IEnumerable<IEventHandler> Resolve();
    }
}