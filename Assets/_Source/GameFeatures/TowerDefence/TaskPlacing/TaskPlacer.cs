using System.Linq;
using Core.DataLoading;
using GameFeatures.TowerDefence.Configs;
using GameFeatures.TowerDefence.ScheduleGeneration;
using UnityEngine;

namespace GameFeatures.TowerDefence.TaskPlacing
{
    public class TaskPlacer
    {
        private readonly TaskPlacingPredictionView _taskPlacingPredictionView;
        private readonly ScheduleView _scheduleView;
        private readonly ScheduleGrid _scheduleGrid;
        private readonly TaskCreator _taskCreator;
        
        public TaskPlacer(ScheduleView scheduleView, ScheduleGrid scheduleGrid, 
            TaskPlacingPredictionView taskPlacingPredictionView, TaskCreator taskCreator)
        {
            _taskCreator = taskCreator;
            _scheduleGrid = scheduleGrid;
            _scheduleView = scheduleView;
            _taskPlacingPredictionView = taskPlacingPredictionView;
            SubscribeOnInput();
        }

        public void SubscribeOnInput()
        {
            _scheduleView.RaycastInputReceiver.OnRayPointed += ShowPlacePrediction;
            _scheduleView.RaycastInputReceiver.OnRayPointedEnd += EndPlacePrediction;
            _scheduleView.RaycastInputReceiver.OnInputStarted += TryPlaceTask;
        }

        public void UnsubscribeFromInput()
        {
            _scheduleView.RaycastInputReceiver.OnRayPointed -= ShowPlacePrediction;
            _scheduleView.RaycastInputReceiver.OnRayPointedEnd -= EndPlacePrediction;
            _scheduleView.RaycastInputReceiver.OnInputStarted -= TryPlaceTask;
        }
        
        private void EndPlacePrediction(Vector3 inputPosition)
        {
            _taskPlacingPredictionView.HidePlacingPrediction();
        }
        
        private void ShowPlacePrediction(Vector3 inputPosition)
        {
            if(!_scheduleGrid.TryGetGridPosition(inputPosition, out Vector3 gridPosition))
            {
                EndPlacePrediction(inputPosition);
                return;
            }
            
            _taskPlacingPredictionView.ShowPlacingPrediction(gridPosition);
        }

        private void TryPlaceTask(Vector3 inputPosition)
        {
            if(!_scheduleGrid.TryGetGridPosition(inputPosition, out Vector3 gridPosition)) return;
            Task task = _taskCreator.Create();
            task.View.transform.position = gridPosition;
        }
    }
}