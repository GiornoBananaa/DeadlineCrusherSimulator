namespace Core.DataSave.Core
{
    public interface ILoadable<in T>
    {
        public string GetSaveKey();
        public void LoadData(T data);
    }
}