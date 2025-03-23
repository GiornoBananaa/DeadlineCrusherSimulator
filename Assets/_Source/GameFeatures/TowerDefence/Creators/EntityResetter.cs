using System.Linq;
using Core.ObjectContainer;
using GameFeatures.WorkProgress;

namespace GameFeatures.TowerDefence
{
    public class EntityResetter : IResettable
    {
        private readonly ObjectContainer<Task> _tasksContainer;
        private readonly ObjectContainer<Deadline> _deadlineContainer;
        private readonly ObjectContainer<TaskProjectile> _projectileContainer;
        
        public EntityResetter(ObjectContainer<Task> tasksContainer, ObjectContainer<Deadline> deadlineContainer, 
            ObjectContainer<TaskProjectile> projectileContainer)
        {
            _tasksContainer = tasksContainer;
            _deadlineContainer = deadlineContainer;
            _projectileContainer = projectileContainer;
        }
        
        public void Reset()
        {
            ResetContainer(_tasksContainer);
            ResetContainer(_deadlineContainer);
            ResetContainer(_projectileContainer);
        }

        private void ResetContainer<T>(ObjectContainer<T> container)
        {
            foreach (var obj in container.Objects.ToArray())
            {
                container.Remove(obj);
            }
        }
    }
}