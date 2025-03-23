using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Core.DataLoading;
using Core.Factory;
using Core.Generation;
using GameFeatures.TowerDefence.Configs;
using GameFeatures.TowerDefence.TaskPlacing;
using GameFeatures.WorkProgress;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameFeatures.TowerDefence.ScheduleGeneration
{
    public class DeadlinesGenerator : IObjectGenerator, IResettable
    {
        private readonly IPoolFactory<Deadline> _deadlineFactory;
        private readonly ScheduleGrid _scheduleGrid;
        private readonly float _generationCooldownMultiplierAfterSpawn;
        private readonly float _generationCooldownDefault;
        private readonly float _last;
        private float _generationCooldown;
        
        private CancellationTokenSource _generationCancellationToken;
        
        public DeadlinesGenerator(IRepository<ScriptableObject> repository, 
            IPoolFactory<Deadline> deadlineFactory, ScheduleGrid scheduleGrid)
        {
            GenerationConfig generationConfig = repository.GetItem<GenerationConfig>().FirstOrDefault();
            if (generationConfig == null)
                throw new NullReferenceException("No GenerationConfig found");
            
            _generationCooldownDefault = generationConfig.DeadlinesGenerationCooldown;
            _generationCooldown = _generationCooldownDefault;
            _generationCooldownMultiplierAfterSpawn = generationConfig.GenerationCooldownMultiplierAfterSpawn;
            _deadlineFactory = deadlineFactory;
            _scheduleGrid = scheduleGrid;
        }
    
        public void StartGeneration()
        {
            _generationCancellationToken = new CancellationTokenSource();
            _ = GenerationLoop(_generationCancellationToken.Token);
        }
        
        public void StopGeneration()
        {
            _generationCancellationToken?.Cancel();
        }
        
        public void Reset()
        {
            _generationCooldown = _generationCooldownDefault;
        }
        
        private async UniTask GenerationLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await UniTask.WaitForSeconds(_generationCooldown, cancellationToken: cancellationToken);
                Deadline deadline = _deadlineFactory.Create();
                _scheduleGrid.SnapToGrid(deadline.View.transform,
                    new Vector3(_scheduleGrid.MaxX, Random.Range(0, _scheduleGrid.MaxY)));
                deadline.View.transform.localRotation = quaternion.identity;

                _generationCooldown *= _generationCooldownMultiplierAfterSpawn;
            }
        }
    }
}