using System.IO;
using Core.DataSave.Core;
using UnityEngine;

namespace Core.DataSave
{
    public class JsonLoader : IGameDataLoader
    {
        public bool TryLoad<T>(ILoadable<T> loadable, string rootPath = null)
        {
            string path = rootPath ?? SaveConstants.SavePathRoot + loadable.GetSaveKey();
            
            if (File.Exists(path))
            {
                string loadPlayerData = File.ReadAllText(path);
                T data = JsonUtility.FromJson<T>(loadPlayerData);
                if(data == null) 
                    return false;
                loadable.LoadData(data);
            }
            else
                return false;
            
            return true;
        }
    }
}