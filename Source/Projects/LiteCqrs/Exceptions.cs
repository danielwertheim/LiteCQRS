using System;
using LiteCqrs.Commanding;
using LiteCqrs.Eventing;

namespace LiteCqrs
{
    internal static class Exceptions
    {
        internal static Exception CanNotAddCommandHandlerRegistration(ICommandHandler registration)
        {
            return new Exception(string.Format("Handler registration for command type: '{0}' could not be registrered.", registration.CommandType.Name));
        }

        internal static Exception CanNotAddEventHandlerRegistration(IEventHandler registration)
        {
            return new Exception(string.Format("Handler registration for command type: '{0}' could not be registrered.", registration.EventType.Name));
        }
    }
}