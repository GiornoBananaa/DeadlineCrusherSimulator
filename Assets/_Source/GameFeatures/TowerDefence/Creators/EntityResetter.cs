using System.Linq;
using Core.Factory;
using Core.ObjectContainer;
using GameFeatures.WorkProgress;

namespace GameFeatures.TowerDefence
{
    public class EntityResetter : IResettable
    {
        private readonly ObjectContainer<Task> _tasksContainer;
        private readonly ObjectContainer<Deadline> _deadlineContainer;
        private readonly ObjectContainer<TaskProjectile> _projectileContainer;
        private readonly IPoolFactory<Task> _tasksPoolFactory;
        private readonly IPoolFactory<Deadline> _deadlinePoolFactory;
        private readonly IPoolFactory<TaskProjectile> _projectilePoolFactory;
        
        public EntityResetter(ObjectContainer<Task> tasksContainer, ObjectContainer<Deadline> deadlineContainer, 
            ObjectContainer<TaskProjectile> projectileContainer, IPoolFactory<Task> tasksPoolFactory,
            IPoolFactory<Deadline> deadlinePoolFactory, IPoolFactory<TaskProjectile> projectilePoolFactory)
        {
            _tasksContainer = tasksContainer;
            _deadlineContainer = deadlineContainer;
            _projectileContainer = projectileContainer;
            _tasksPoolFactory = tasksPoolFactory;
            _deadlinePoolFactory = deadlinePoolFactory;
            _projectilePoolFactory = projectilePoolFactory;
        }
        
        public void Reset()
        {
            ResetContainer(_tasksContainer,_tasksPoolFactory);
            ResetContainer(_deadlineContainer,_deadlinePoolFactory);
            ResetContainer(_projectileContainer,_projectilePoolFactory);
        }

        private void ResetContainer<T>(ObjectContainer<T> container, IPoolFactory<T> poolFactory)
        {
            foreach (var obj in container.Objects.ToArray())
            {
                poolFactory.Release(obj);
            }
        }
    }
}