using Core.EntitySystem;
using UnityEngine;

namespace Core.EntitySystem
{
    public class GameEntityLinker : MonoBehaviour
    {
        public IGameEntity GameEntity { get; private set; }
        
        public void LinkEntity(IGameEntity entity)
        {
            GameEntity = entity;
        }
    }
}