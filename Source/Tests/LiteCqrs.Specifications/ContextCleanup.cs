using Machine.Specifications;

namespace LiteCqrs.Specifications
{
    public class ContextCleanup : ICleanupAfterEveryContextInAssembly
    {
        public void AfterContextCleanup()
        {
        }
    }
}