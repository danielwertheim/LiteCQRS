namespace LiteCqrs.Commanding
{
    public interface ICommandBus
    {
        void Send<T>(T command) where T : ICommand;
    }
}