using System.Collections.Generic;
using Core.SceneEventSystem;
using UnityEngine;

namespace Core.DataSave
{
    public class SceneEventSaveInvoker : IOnSceneStartListener, IOnSceneDestroyListener
    {
        private readonly IEnumerable<IDataSaveProvider> _saveProviders;
        private readonly IEnumerable<IDataLoadProvider> _loadProviders;

        public SceneEventSaveInvoker(IEnumerable<IDataSaveProvider> saveProviders, IEnumerable<IDataLoadProvider> loadProviders)
        {
            _saveProviders = saveProviders;
            _loadProviders = loadProviders;
        }
        
        public void OnSceneStart()
        {
            foreach (var loadProvider in _loadProviders)
            {
                loadProvider.LoadAll();
            }
        }

        public void OnSceneDestroy()
        {
            foreach (var saveProvider in _saveProviders)
            {
                saveProvider.SaveAll();
            }
        }
    }
}