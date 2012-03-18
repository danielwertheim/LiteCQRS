using System;
using Machine.Specifications;

namespace LiteCqrs.Specifications
{
    public abstract class SpecificationBase
    {
        protected static ITestContext TestContext;

        protected static Exception CaughtException;

        Cleanup after = () =>
        {
            if (TestContext != null)
                TestContext.Cleanup();

            CaughtException = null;
        };
    }
}