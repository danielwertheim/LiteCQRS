namespace LiteCqrs.Specifications
{
    public interface ITestContext
    {
        ICqrsRuntime CqrsRuntime { get; set; }
        void Cleanup();
    }
}