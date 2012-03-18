using System;
using LiteCqrs.Commanding;

namespace LiteCqrs.TestScenario.Commands
{
    [Serializable]
    public class MySubCommand : ICommand
    {
        public Guid AggregateId { get; private set; }
        public string Value { get; private set; }

        public MySubCommand(Guid aggregateId, string value)
        {
            AggregateId = aggregateId;
            Value = value;
        }
    }
}