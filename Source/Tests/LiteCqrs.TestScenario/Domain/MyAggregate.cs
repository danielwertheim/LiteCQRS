using System;
using LiteCqrs.Domain;
using LiteCqrs.Eventing;
using LiteCqrs.TestScenario.Events;

namespace LiteCqrs.TestScenario.Domain
{
    public class MyAggregate : AggregateRoot
    {
        [ThreadStatic]
        public static Action<IEvent> OnEventApplierInvocation;

        private string _value;

        public MyAggregate(Guid aggregateId)
        {
            Raise(new MyAggregateCreatedEvent(aggregateId));
        }

        protected void OnMyAggregateCreated(MyAggregateCreatedEvent e)
        {
            Id = e.AggregateRootId;
        }

        public void ActionRaisingEventWithApplier(string value)
        {
            Raise(new MyEvent(Id, value));
        }

        protected void OnAsLongAsPrefixOnIsUsedApplierCouldBeNamedWhatever(MyEvent e)
        {
            if(OnEventApplierInvocation != null)
                OnEventApplierInvocation.Invoke(e);

            _value = e.Value;
        }

        public void ActionRaisingSubEventWithApplier(string value)
        {
            Raise(new MySubEvent(Id, value));
        }

        protected void OnAsLongAsPrefixOnIsUsedApplierCouldBeNamedWhatever(MySubEvent e)
        {
            if (OnEventApplierInvocation != null)
                OnEventApplierInvocation.Invoke(e);

            _value = e.Value;
        }
    }
}