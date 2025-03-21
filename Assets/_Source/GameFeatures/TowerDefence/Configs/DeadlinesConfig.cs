﻿using UnityEngine;

namespace GameFeatures.TowerDefence.Configs
{
    [CreateAssetMenu(menuName = "Configs/TowerDefence/DeadlinesConfig")]
    public class DeadlinesConfig : ScriptableObject
    {
        [field: SerializeField] public DeadlineView Prefab { get; private set; }
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public LayerMask DamageableLayerMask { get; private set; }
        [field: SerializeField] public Vector3 MoveDirection { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
    }
}