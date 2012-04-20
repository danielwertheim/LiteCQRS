using System;
using LiteCqrs.Eventing;
using LiteCqrs.TestScenario.Events;

namespace LiteCqrs.TestScenario.Denormalizers
{
    public class MyDenormalizers
    {
        [ThreadStatic]
        public static Action<IEvent> OnHandlerInvocation;

        public void ThisCouldBeNamedWhatEver(MyEvent e)
        {
            if (OnHandlerInvocation != null)
                OnHandlerInvocation.Invoke(e);
        }

        public void ASecondHandlerForSameEvent(MyEvent e)
        {
            if (OnHandlerInvocation != null)
                OnHandlerInvocation.Invoke(e);
        }
    }
}