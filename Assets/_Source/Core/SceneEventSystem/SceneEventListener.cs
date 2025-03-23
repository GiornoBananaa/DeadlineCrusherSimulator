using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.SceneEventSystem
{
    public class SceneEventListener : MonoBehaviour
    {
        private IEnumerable<IOnSceneStartListener> _onStartListeners;
        private IEnumerable<IOnSceneDestroyListener> _onDestroyListeners;
        
        [Inject]
        public void Construct(IEnumerable<IOnSceneStartListener> onSceneStartListeners, 
            IEnumerable<IOnSceneDestroyListener> onSceneDestroyListeners)
        {
            _onStartListeners = onSceneStartListeners;
            _onDestroyListeners = onSceneDestroyListeners;
        }
        
        private void Start()
        {
            foreach (var startListener in _onStartListeners)
            {
                startListener.OnSceneStart();
            }
        }

        private void OnDestroy()
        {
            foreach (var destroyListener in _onDestroyListeners)
            {
                destroyListener.OnSceneDestroy();
            }
        }
    }
}
