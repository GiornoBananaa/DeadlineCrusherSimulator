namespace Core.DataSave.Core
{
    public interface ISavable<out T>
    {
        public string GetSaveKey();
        public T GetSaveData();
    }
}