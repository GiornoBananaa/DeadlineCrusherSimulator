using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameFeatures.Clicker
{
    public class TextWriterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private int _maxLinesCount;
        
        private readonly Queue<string> _textQueue = new Queue<string>();
        
        public void AddLine(string line)
        {
            _textQueue.Enqueue(line);
            
            if(_textQueue.Count > _maxLinesCount)
                _textQueue.Dequeue();
            
            UpdateText();
        }
        
        public void Clear()
        {
            _textQueue.Clear();
            
            UpdateText();
        }
        
        private void UpdateText()
        {
            string text = "";
            foreach (string line in _textQueue)
            {
                text += line + "\n";
            }
            _text.text = text;
        }
    }
}