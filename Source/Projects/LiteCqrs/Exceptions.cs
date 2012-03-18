using System;
using LiteCqrs.Commanding;
using LiteCqrs.Eventing;

namespace LiteCqrs
{
    internal static class Exceptions
    {
        internal static Exception CanNotAddCommandHandlerRegistration(ICommandHandler registration)
        {
            return new Exception(string.Format("Handler registration for command type: '{0}' could not be registrered. Only one command handler per command and class are allowed.", registration.CommandType.Name));
        }

        internal static Exception CanNotAddEventHandlerRegistration(IEventHandler registration)
        {
            return new Exception(string.Format("Handler registration for command type: '{0}' could not be registrered.", registration.EventType.Name));
        }

        internal static Exception CanNotLocateCommandHandler(Type commandType)
        {
            return new Exception(string.Format("Handler for command type: '{0}' could not be located.", commandType.Name));
        }
    }
}