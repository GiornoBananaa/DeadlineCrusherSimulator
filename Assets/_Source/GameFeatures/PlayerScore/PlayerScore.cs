using System;
using Core.DataSave.Core;
using GameFeatures.WorkProgress;
using UnityEngine;

namespace GameFeatures.PlayerScore
{
    public class PlayerScore : ISavable<ScoreData>, ILoadable<ScoreData>, IResettable
    {
        public int Score { get; private set; }
        public int Record { get; private set; }
        
        public event Action<int> OnScoreChanged;
        public event Action<int> OnRecordChanged;
        
        public void AddScore()
        {
            Score++;
            OnScoreChanged?.Invoke(Score);

            if (Score > Record)
            {
                Record = Score;
                OnRecordChanged?.Invoke(Record);
            }
        }

        public void Reset()
        {
            Score = 0;
            OnScoreChanged?.Invoke(Score);
        }
        
        #region Saving

        public string GetSaveKey() => "PlayerScore";

        public ScoreData GetSaveData()
            => new()
            {
                Record = Record 
            };
        
        public void LoadData(ScoreData data)
        {
            Record = data.Record;
            OnRecordChanged?.Invoke(Record);
        }

        #endregion
    }

    public class ScoreData
    {
        public int Record;
    }
}