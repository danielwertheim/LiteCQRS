using System;
using LiteCqrs.Eventing;
using LiteCqrs.TestScenario.Events;

namespace LiteCqrs.TestScenario.SubEventHandlers.SubA
{
    public class AllSubEventHandlers
    {
        [ThreadStatic]
        public static Action<IEvent> OnHandlerInvocation;

        public void ThisCouldBeNamedWhatEver(MySubEvent e)
        {
            if (OnHandlerInvocation != null)
                OnHandlerInvocation.Invoke(e);
        }

        public void ASecondHandlerForSameEvent(MySubEvent e)
        {
            if (OnHandlerInvocation != null)
                OnHandlerInvocation.Invoke(e);
        }
    }
}