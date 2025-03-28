﻿using Core.EntitySystem;
using Core.PhysicsDetection;
using UnityEngine;
using Zenject;

namespace GameFeatures.TowerDefence
{
    public class DeadlineView : GameEntityLinker
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field: SerializeField] public CollisionDetector CollisionDetector { get; private set; }
    }
}