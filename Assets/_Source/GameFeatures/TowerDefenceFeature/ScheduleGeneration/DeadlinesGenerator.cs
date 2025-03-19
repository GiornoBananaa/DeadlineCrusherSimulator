using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameFeatures.TowerDefenceFeature.Configs;
using Core.DataLoading;
using Core.Factory;
using Core.Generation;
using Unity.Mathematics;
using UnityEngine;

namespace GameFeatures.TowerDefenceFeature.ScheduleGeneration
{
    public class DeadlinesGenerator : IObjectGenerator
    {
        private readonly Transform[] _spawnPoints;
        private readonly PoolFactory<Deadline> _deadlineFactory;
        private readonly ScheduleView _scheduleView;
        private readonly float _generationCooldown;
        
        private CancellationTokenSource _generationCancellationToken;
        
        public DeadlinesGenerator(IRepository<ScriptableObject> repository, ScheduleView scheduleView, PoolFactory<Deadline> deadlineFactory)
        {
            GenerationConfig generationConfig = repository.GetItem<GenerationConfig>().FirstOrDefault();
            if (generationConfig == null)
                throw new NullReferenceException("No GenerationConfig found");
            
            _generationCooldown = generationConfig.DeadlinesGenerationCooldown;
            _spawnPoints = scheduleView.LineEndPoints;
            _deadlineFactory = deadlineFactory;
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
                deadline.View.transform.parent = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)];
                deadline.View.transform.localPosition = Vector3.zero;
                deadline.View.transform.localRotation = quaternion.identity;
            }
        }
    }
}