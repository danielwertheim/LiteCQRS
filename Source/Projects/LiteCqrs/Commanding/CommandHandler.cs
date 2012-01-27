using System;
using System.Reflection;

namespace LiteCqrs.Commanding
{
    public class CommandHandler : ICommandHandler
    {
		public Type ContainerType { get; private set; }
    	public Type CommandType { get; private set; }
		public MethodInfo Handler { get; private set; }

    	public CommandHandler(Type containerType, Type commandType, MethodInfo handler)
    	{
    		ContainerType = containerType;
    		CommandType = commandType;
    		Handler = handler;
    	}
    }
}