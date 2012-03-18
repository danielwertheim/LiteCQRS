using LiteCqrs.TestScenario.CommandHandlers;
using LiteCqrs.TestScenario.Denormalizers;
using LiteCqrs.TestScenario.Domain;
using LiteCqrs.TestScenario.EventHandlers;
using LiteCqrs.TestScenario.SubCommandHandlers.SubA;

namespace LiteCqrs.Specifications
{
    public class TestContext : ITestContext
    {
        public ICqrsRuntime CqrsRuntime { get; set; }

        public TestContext(ICqrsRuntime cqrsRuntime)
        {
            CqrsRuntime = cqrsRuntime;
        }

        public void Cleanup()
        {
            AllCommandHandlers.OnHandlerInvocation = null;
            AllSubCommandHandlers.OnHandlerInvocation = null;
            AllEventHandlers.OnHandlerInvocation = null;
            AllDenormalizers.OnHandlerInvocation = null;
            MyAggregate.OnEventApplierInvocation = null;
        }
    }
}