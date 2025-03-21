using UnityEngine;

namespace GameFeatures.Clicker.Configs
{
    [CreateAssetMenu(menuName = "Configs/Clicker/CodeWritingConfig")]
    public class CodeWritingConfig : ScriptableObject
    {
        [SerializeField] private TextAsset _textAsset;

        public string[] TextLines;
        
        public void ApplyTextAsset()
        {
            TextLines = _textAsset.text.Split('\n');
        }
    }
}