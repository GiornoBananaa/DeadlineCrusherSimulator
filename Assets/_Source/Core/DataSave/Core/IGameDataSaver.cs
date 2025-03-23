namespace Core.DataSave.Core
{
    public interface IGameDataSaver
    {
        void Save<T>(ISavable<T> data, string path = null);
    }
}