using System.Collections.Generic;
using Core.DataSave.Core;
using UnityEngine;

namespace Core.DataSave
{
    public class SaveProvider<T> : IDataSaveProvider, IDataLoadProvider
    {
        private readonly IGameDataLoader _dataLoader;
        private readonly IGameDataSaver _dataSaver;
    
        private readonly IEnumerable<ISavable<T>> _savable;
        private readonly IEnumerable<ILoadable<T>> _loadable;
        
        public SaveProvider(IGameDataLoader dataLoader, IGameDataSaver dataSaver, 
            IEnumerable<ISavable<T>> savable, IEnumerable<ILoadable<T>> loadable)
        {
            _dataLoader = dataLoader;
            _dataSaver = dataSaver;
            _savable = savable;
            _loadable = loadable;
        }

        public void Save(ISavable<T> data)
        {
            _dataSaver.Save(data);
        }
        
        public void SaveAll()
        {
            foreach (var savable in _savable)
            {
                _dataSaver.Save(savable);
            }
        }
        
        public void LoadAll()
        {
            foreach (var loadable in _loadable)
            {
                _dataLoader.TryLoad(loadable);
            }
        }
    }
}