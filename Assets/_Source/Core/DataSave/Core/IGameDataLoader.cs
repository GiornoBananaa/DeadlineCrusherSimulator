namespace Core.DataSave.Core
{
    public interface IGameDataLoader
    {
        bool TryLoad<T>(ILoadable<T> loadable, string rootPath = null);
    }
}