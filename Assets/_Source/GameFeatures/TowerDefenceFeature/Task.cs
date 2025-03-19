using UnityEngine;

namespace GameFeatures.TowerDefenceFeature
{
    public class Task
    {
        public TaskView View { get; set; }
    }
    
    public class Deadline
    {
        public DeadlineView View { get; set; }
        public Vector3 MoveDirection { get; set; }
        public float MoveSpeed { get; set; }
    }
}