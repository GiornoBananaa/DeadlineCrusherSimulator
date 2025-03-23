using System.IO;
using Core.DataSave.Core;
using UnityEngine;

namespace Core.DataSave
{
    public class JsonSaver : IGameDataSaver
    {
        public void Save<T>(ISavable<T> loadable, string rootPath = null)
        {
            string path = rootPath ?? SaveConstants.SavePathRoot + loadable.GetSaveKey();
            string savePlayerData = JsonUtility.ToJson(loadable.GetSaveData());
            File.WriteAllText(path, savePlayerData);
        }
    }
}