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
        private readonly PoolFactory<Deadline> _deadlineFactory;
        private readonly ScheduleView _scheduleView;
        private readonly ScheduleGrid _scheduleGrid;
        private readonly float _generationCooldown;
        
        private CancellationTokenSource _generationCancellationToken;
        
        public DeadlinesGenerator(IRepository<ScriptableObject> repository, ScheduleView scheduleView, 
            PoolFactory<Deadline> deadlineFactory, ScheduleGrid scheduleGrid)
        {
            GenerationConfig generationConfig = repository.GetItem<GenerationConfig>().FirstOrDefault();
            if (generationConfig == null)
                throw new NullReferenceException("No GenerationConfig found");
            
            _generationCooldown = generationConfig.DeadlinesGenerationCooldown;
            _scheduleView = scheduleView; 
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
                deadline.View.transform.parent = _scheduleView.GridPivot;
                deadline.View.transform.localPosition = _scheduleGrid.GetGridPositionClamped(new Vector3(Random.Range(0, _scheduleGrid.MaxX), _scheduleGrid.MaxY));
                deadline.View.transform.localRotation = quaternion.identity;
            }
        }
    }
}