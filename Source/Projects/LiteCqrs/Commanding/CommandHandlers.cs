using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using EnsureThat;

namespace LiteCqrs.Commanding
{
    public class CommandHandlers
    {
    	protected readonly ConcurrentDictionary<Type, ICommandHandler> Registrations;

        public CommandHandlers()
        {
            Registrations = new ConcurrentDictionary<Type, ICommandHandler>();
        }

        public virtual void Clear()
        {
            Registrations.Clear();
        }

        public virtual void Register(IEnumerable<ICommandHandler> handlers)
        {
        	Ensure.That(handlers, "handlers").IsNotNull();

            foreach (var handler in handlers)
            {
                if (!Registrations.TryAdd(handler.CommandType, handler))
                    throw Exceptions.CanNotAddCommandHandlerRegistration(handler);
            }
        }

    	public virtual ICommandHandler Get(Type commandType)
    	{
    	    ICommandHandler handler;
    		if(!Registrations.TryGetValue(commandType, out handler) || handler == null)
                throw Exceptions.CanNotLocateCommandHandler(commandType);

    	    return handler;
    	}
    }
}