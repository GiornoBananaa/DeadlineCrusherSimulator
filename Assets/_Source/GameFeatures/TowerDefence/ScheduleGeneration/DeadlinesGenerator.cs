using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Core.DataLoading;
using Core.Factory;
using Core.Generation;
using GameFeatures.TowerDefence.Configs;
using GameFeatures.TowerDefence.TaskPlacing;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameFeatures.TowerDefence.ScheduleGeneration
{
    public class DeadlinesGenerator : IObjectGenerator
    {
        private readonly IPoolFactory<Deadline> _deadlineFactory;
        private readonly ScheduleGrid _scheduleGrid;
        private readonly float _generationCooldown;
        private readonly float _last;
        
        private CancellationTokenSource _generationCancellationToken;
        
        public DeadlinesGenerator(IRepository<ScriptableObject> repository, 
            IPoolFactory<Deadline> deadlineFactory, ScheduleGrid scheduleGrid)
        {
            GenerationConfig generationConfig = repository.GetItem<GenerationConfig>().FirstOrDefault();
            if (generationConfig == null)
                throw new NullReferenceException("No GenerationConfig found");
            
            _generationCooldown = generationConfig.DeadlinesGenerationCooldown;
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

        private async UniTask GenerationLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await UniTask.WaitForSeconds(_generationCooldown, cancellationToken: cancellationToken);
                Deadline deadline = _deadlineFactory.Create();
                _scheduleGrid.SnapToGrid(deadline.View.transform,
                    new Vector3(_scheduleGrid.MaxX, Random.Range(0, _scheduleGrid.MaxY)));
                deadline.View.transform.localRotation = quaternion.identity;
            }
        }
    }
}