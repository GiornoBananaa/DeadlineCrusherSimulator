using TMPro;
using UnityEngine;
using Zenject;

namespace GameFeatures.PlayerScore
{
    public class ScorePanelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _recordText;

        private PlayerScore _playerScore;
        
        [Inject]
        public void Construct(PlayerScore playerScore)
        {
            _playerScore = playerScore;
            _playerScore.OnScoreChanged += SetScore;
            _playerScore.OnRecordChanged += SetRecord;
        }

        private void Awake()
        {
            _scoreText.text = "0";
            _recordText.text = "0";
        }

        private void SetScore(int score)
        {
            _scoreText.text = score.ToString();
        }
        
        private void SetRecord(int record)
        {
            _recordText.text = record.ToString();
        }
    }
}