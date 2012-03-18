using System;
using LiteCqrs.Commanding;

namespace LiteCqrs.TestScenario.Commands
{
    [Serializable]
    public class MyCommand : ICommand
    {
        public Guid AggregateId { get; private set; }
        public string Value { get; private set; }

        public MyCommand(Guid aggregateId, string value)
        {
            AggregateId = aggregateId;
            Value = value;
        }
    }
}