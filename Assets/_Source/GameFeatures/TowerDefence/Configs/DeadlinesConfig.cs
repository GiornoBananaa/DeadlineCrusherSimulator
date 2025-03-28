﻿using UnityEngine;

namespace GameFeatures.TowerDefence.Configs
{
    [CreateAssetMenu(menuName = "Configs/TowerDefence/DeadlinesConfig")]
    public class DeadlinesConfig : ScriptableObject
    {
        [field: SerializeField] public DeadlineView Prefab { get; private set; }
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public LayerMask DamageableLayerMask { get; private set; }
        [field: SerializeField] public LayerMask ExpirationLineLayerMask { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float SelfDamage { get; private set; }
    }
}