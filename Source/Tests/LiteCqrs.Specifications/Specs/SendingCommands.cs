using System;
using System.Collections.Generic;
using System.Linq;
using LiteCqrs.Commanding;
using LiteCqrs.Eventing;
using LiteCqrs.TestScenario.CommandHandlers;
using LiteCqrs.TestScenario.Commands;
using LiteCqrs.TestScenario.Denormalizers;
using LiteCqrs.TestScenario.Domain;
using LiteCqrs.TestScenario.EventHandlers;
using LiteCqrs.TestScenario.Events;
using LiteCqrs.TestScenario.SubCommandHandlers.SubA;
using LiteCqrs.TestScenario.SubDenormalizers.SubA;
using LiteCqrs.TestScenario.SubEventHandlers.SubA;
using Machine.Specifications;

namespace LiteCqrs.Specifications.Specs
{
    [Subject(typeof(ICommandBus), "Send Command")]
    public class when_commandhandler_and_two_eventhandlers_and_two_denormalizers_exists : SpecificationBase
    {
        Establish context = () =>
        {
            TestContext = TestContextFactory.CreateDefault();
            MyCommandHandlers.OnHandlerInvocation = cmd => _interceptedCommand = cmd;
            MyAggregate.OnEventApplierInvocation = e => _interceptedEventApplierEvents.Add(e);
            MyEventHandlers.OnHandlerInvocation = e => _interceptedEventHandlerEvents.Add(e);
            MyDenormalizers.OnHandlerInvocation = e => _interceptedDenormalizerEvents.Add(e);
            _initialCommand = new MyCommand(Guid.NewGuid(), "Test");
        };

        Because of = 
            () => TestContext.CqrsRuntime.CommandBus.Send(_initialCommand);

        It should_have_invoked_command_handler = 
            () => _interceptedCommand.ShouldNotBeNull();

        It should_have_invoked_event_applier_in_aggregate =
            () => _interceptedEventApplierEvents.OfType<MyEvent>().SingleOrDefault().ShouldNotBeNull();

        It should_have_invoked_both_event_handlers =
            () => _interceptedEventHandlerEvents.OfType<MyEvent>().Count().ShouldEqual(2);

        It should_have_invoked_both_denormalizers =
            () => _interceptedEventHandlerEvents.OfType<MyEvent>().Count().ShouldEqual(2);

        private static MyCommand _initialCommand;
        private static ICommand _interceptedCommand;
        private static List<IEvent> _interceptedEventApplierEvents = new List<IEvent>();
        private static List<IEvent> _interceptedEventHandlerEvents = new List<IEvent>();
        private static List<IEvent> _interceptedDenormalizerEvents = new List<IEvent>();
    }

    [Subject(typeof(ICommandBus), "Send Command")]
    public class when_commandhandler_and_two_eventhandlers_and_two_denormalizers_exists_in_sub_namespaces : SpecificationBase
    {
        Establish context = () =>
        {
            TestContext = TestContextFactory.CreateDefault();
            MySubCommandHandlers.OnHandlerInvocation = cmd => _interceptedCommand = cmd;
            MyAggregate.OnEventApplierInvocation = e => _interceptedEventApplierEvents.Add(e);
            MySubEventHandlers.OnHandlerInvocation = e => _interceptedEventHandlerEvents.Add(e);
            MySubDenormalizers.OnHandlerInvocation = e => _interceptedDenormalizerEvents.Add(e);
            _initialCommand = new MySubCommand(Guid.NewGuid(), "Test");
        };

        Because of =
            () => TestContext.CqrsRuntime.CommandBus.Send(_initialCommand);

        It should_have_invoked_command_handler =
            () => _interceptedCommand.ShouldNotBeNull();

        It should_have_invoked_event_applier_in_aggregate =
            () => _interceptedEventApplierEvents.OfType<MySubEvent>().SingleOrDefault().ShouldNotBeNull();

        It should_have_invoked_both_event_handlers =
            () => _interceptedEventHandlerEvents.OfType<MySubEvent>().Count().ShouldEqual(2);

        It should_have_invoked_both_denormalizers =
            () => _interceptedEventHandlerEvents.OfType<MySubEvent>().Count().ShouldEqual(2);

        private static MySubCommand _initialCommand;
        private static ICommand _interceptedCommand;
        private static List<IEvent> _interceptedEventApplierEvents = new List<IEvent>();
        private static List<IEvent> _interceptedEventHandlerEvents = new List<IEvent>();
        private static List<IEvent> _interceptedDenormalizerEvents = new List<IEvent>();
    }
}