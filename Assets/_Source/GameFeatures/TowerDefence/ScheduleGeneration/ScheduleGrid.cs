using System;
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
        
        public ScheduleGrid(IRepository<ScriptableObject> dataRepository, ScheduleView scheduleView)
        {
            ScheduleGridConfig config = dataRepository.GetItem<ScheduleGridConfig>().FirstOrDefault();
            if(config == null) return;
            
            _linesCount = config.LinesCount;
            _rowsCount = config.RowsCount;
            _cellXSize = config.CellXSize;
            _cellYSize = config.CellYSize;
            
            _scheduleView = scheduleView;
        }
        
        public bool IsOnGrid(Vector3 position)
        {
            Vector3 localGridPosition = _scheduleView.GridPivot.InverseTransformPoint(position);
            
            int line = (int)(localGridPosition.y / _cellYSize);
            int row =  (int)(localGridPosition.x / _cellXSize);
            
            return line >= 0 && row >= 0 && line < _linesCount && row < _rowsCount;
        }
        
        public void SnapToGrid(Transform transform, int minLine = int.MinValue, int maxLine = int.MaxValue, int minRow = int.MinValue, int maxRow = int.MaxValue)
        {
            transform.parent = _scheduleView.GridPivot.transform;
            transform.localPosition = GetLocalGridPositionClamped(transform.position, minLine, maxLine, minRow, maxRow);
        }
        
        public void SnapToGrid(Transform transform, Vector3 localPosition, int minLine = int.MinValue, int maxLine = int.MaxValue, int minRow = int.MinValue, int maxRow = int.MaxValue)
        {
            transform.parent = _scheduleView.GridPivot.transform;
            transform.localPosition = GetGridPositionClamped(localPosition, minLine, maxLine, minRow, maxRow);
        }
        
        public Vector3 GetLocalGridPositionClamped(Vector3 worldPosition, int minLine = int.MinValue, int maxLine = int.MaxValue, int minRow = int.MinValue, int maxRow = int.MaxValue) 
            => GetGridPositionClamped(_scheduleView.GridPivot.InverseTransformPoint(worldPosition), minLine, maxLine, minRow, maxRow);
        
        public Vector3 GetGridPositionClamped(Vector3 localGridPosition, int minLine = int.MinValue, int maxLine = int.MaxValue, int minRow = int.MinValue, int maxRow = int.MaxValue)
        {
            int line = (int)Mathf.Round(Mathf.Clamp(localGridPosition.y / _cellYSize, Mathf.Max(0, minLine), Mathf.Min(maxLine, _linesCount) - 1));
            int row =  (int)Mathf.Round(Mathf.Clamp(localGridPosition.x / _cellXSize, Mathf.Max(0, minRow), Mathf.Min(maxRow, _rowsCount) - 1));
            
            return new Vector3(row * _cellXSize, line * _cellYSize, 0);
        }
    }
}