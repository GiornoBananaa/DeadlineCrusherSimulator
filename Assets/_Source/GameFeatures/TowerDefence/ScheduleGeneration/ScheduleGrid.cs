using System.Linq;
using Core.DataLoading;
using GameFeatures.TowerDefence.Configs;
using GameFeatures.TowerDefence.ScheduleGeneration;
using UnityEngine;

namespace GameFeatures.TowerDefence.TaskPlacing
{
    public class ScheduleGrid
    {
        private readonly ScheduleView _scheduleView;
        private readonly float _linesCount;
        private readonly float _rowsCount;
        private readonly float _cellXSize;
        private readonly float _cellYSize;

        public float MaxX => _cellXSize * _rowsCount;
        public float MaxY => _cellYSize * _linesCount;
        
        public ScheduleGrid(DataRepository<ScriptableObject> dataRepository, ScheduleView scheduleView)
        {
            ScheduleGridConfig config = dataRepository.GetItem<ScheduleGridConfig>().FirstOrDefault();
            if(config == null) return;
            
            _scheduleView = scheduleView;
        }
        
        public Vector3 GetGridPositionClamped(Vector3 position)
        {
            Vector3 localGridPosition = _scheduleView.GridPivot.InverseTransformPoint(position);
            
            int line = (int)Mathf.Clamp(localGridPosition.y / _cellYSize, 0, _linesCount);
            int row =  (int)Mathf.Clamp(localGridPosition.x / _cellXSize, 0, _rowsCount);
            
            return new Vector3(row, line, 0);
        }
        
        public bool TryGetGridPosition(Vector3 position, out Vector3 gridPosition)
        {
            Vector3 localGridPosition = _scheduleView.GridPivot.InverseTransformPoint(position);
            
            int line = (int)(localGridPosition.y / _cellYSize);
            int row =  (int)(localGridPosition.x / _cellXSize);

            if (line < 0 || row < 0 || line >= _linesCount || row >= _rowsCount)
            {
                gridPosition = default;
                return false;
            }
            
            gridPosition = new Vector3(line, row, 0);
            return true;
        }
    }
}