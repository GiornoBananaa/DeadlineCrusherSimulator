﻿using UnityEngine;

namespace GameFeatures.TowerDefenceFeature.Configs
{
    [CreateAssetMenu(menuName = "Configs/TowerDefence/TasksConfig")]
    public class TasksConfig : ScriptableObject
    {
        [field: SerializeField] public TaskView Prefab { get; private set; }
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public float ShootingCooldown { get; private set; }
        [field: SerializeField] public Color[] Colors { get; private set; }
    }
}