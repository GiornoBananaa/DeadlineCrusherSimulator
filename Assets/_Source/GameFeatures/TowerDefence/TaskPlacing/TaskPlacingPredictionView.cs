using UnityEngine;

namespace GameFeatures.TowerDefence.TaskPlacing
{
    public class TaskPlacingPredictionView : MonoBehaviour
    {
        [SerializeField] private GameObject _predictionTask;

        public void ShowPlacingPrediction(Vector3 position)
        {
            _predictionTask.transform.position = position;
            _predictionTask.SetActive(true);
        }
        
        public void HidePlacingPrediction()
        {
            _predictionTask.SetActive(false);
        }
    }
}