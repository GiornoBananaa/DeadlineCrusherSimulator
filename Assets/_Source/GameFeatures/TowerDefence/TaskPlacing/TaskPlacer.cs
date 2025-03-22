using System.Linq;
using Core.DataLoading;
using Core.Factory;
using GameFeatures.TowerDefence.Configs;
using GameFeatures.TowerDefence.ScheduleGeneration;
using GameFeatures.WorkProgress;
using UnityEngine;

namespace GameFeatures.TowerDefence.TaskPlacing
{
    public class TaskPlacer
    {
        private readonly TaskPlacingPredictionView _taskPlacingPredictionView;
        private readonly ScheduleView _scheduleView;
        private readonly ScheduleGrid _scheduleGrid;
        private readonly WorkCounter _workCounter;
        private readonly IPoolFactory<Task> _taskCreator;
        private readonly int _maxRow;
        
        public TaskPlacer(IRepository<ScriptableObject> dataRepository, ScheduleView scheduleView, ScheduleGrid scheduleGrid, 
            TaskPlacingPredictionView taskPlacingPredictionView, IPoolFactory<Task> taskCreator, WorkCounter workCounter)
        {
            TasksConfig config = dataRepository.GetItem<TasksConfig>().FirstOrDefault();
            if(config == null) return;
            
            _maxRow = config.MaxRow;
            _taskCreator = taskCreator;
            _scheduleGrid = scheduleGrid;
            _scheduleView = scheduleView;
            _taskPlacingPredictionView = taskPlacingPredictionView;
            _workCounter = workCounter;
            _workCounter.OnWorkIsDone += SubscribeOnInput;
            _workCounter.OnNewWork += UnsubscribeFromInput;
        }

        public void SubscribeOnInput()
        {
            _scheduleView.RaycastInputReceiver.OnRayPointed += ShowPlacePrediction;
            _scheduleView.RaycastInputReceiver.OnRayPointedEnd += HidePlacePrediction;
            _scheduleView.RaycastInputReceiver.OnInputStarted += TryPlaceTask;
        }

        public void UnsubscribeFromInput()
        {
            _scheduleView.RaycastInputReceiver.OnRayPointed -= ShowPlacePrediction;
            _scheduleView.RaycastInputReceiver.OnRayPointedEnd -= HidePlacePrediction;
            _scheduleView.RaycastInputReceiver.OnInputStarted -= TryPlaceTask;
            if(_taskPlacingPredictionView)
                _taskPlacingPredictionView.HidePlacingPrediction();
        }
        
        private void HidePlacePrediction(Vector3 inputPosition)
        {
            _taskPlacingPredictionView.HidePlacingPrediction();
        }
        
        private void ShowPlacePrediction(Vector3 inputPosition)
        {
            if(!_scheduleGrid.IsOnGrid(inputPosition))
            {
                HidePlacePrediction(inputPosition);
                return;
            }
            
            _taskPlacingPredictionView.ShowPlacingPrediction(_scheduleGrid.GetLocalGridPositionClamped(inputPosition, maxRow: _maxRow));
        }

        private void TryPlaceTask(Vector3 inputPosition)
        {
            if(!_scheduleGrid.IsOnGrid(inputPosition) || !_workCounter.TryNextWork()) return;
            Task task = _taskCreator.Create();
            task.View.transform.position = inputPosition;
            _scheduleGrid.SnapToGrid(task.View.transform, maxRow: _maxRow);
        }
    }
}