using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiteCqrs.Eventing
{
    public class InProcAsyncEventPublisher : InProcEventPublisher
    {
        public InProcAsyncEventPublisher(EventHandlers eventHandlers, IEventHandlerInvoker eventHandlerInvoker)
            :base(eventHandlers, eventHandlerInvoker)
        {
        }

        public override void Publish(IEnumerable<IEvent> events)
        {
            Task.Factory.StartNew(() => base.Publish(events));
        }
    }
}