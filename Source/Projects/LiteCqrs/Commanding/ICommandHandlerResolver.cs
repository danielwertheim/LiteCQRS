using System.Collections.Generic;

namespace LiteCqrs.Commanding
{
	/// <summary>
	/// Responsible for resolving and creating all availible
	/// <see cref="ICommandHandler"/> in the domain.
	/// </summary>
    public interface ICommandHandlerResolver
    {
        IEnumerable<ICommandHandler> Resolve();
    }
}