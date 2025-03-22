using UnityEngine;

namespace GameFeatures.Clicker
{
    [CreateAssetMenu(menuName = "Configs/Clicker/CodeWritingConfig")]
    public class CodeWritingConfig : ScriptableObject
    {
        [SerializeField] private TextAsset _textAsset;
        
        [HideInInspector] public string[] TextLines;
        
        public void ApplyTextAsset()
        {
            TextLines = _textAsset.text.Split('\n');
        }
    }
}