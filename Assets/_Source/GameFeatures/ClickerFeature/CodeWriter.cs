using System;
using System.Linq;
using Core.InstallationSystem.DataLoadingSystem;
using GameFeatures.ClickerFeature.Configs;
using GameFeatures.ClickerFeature.Views;
using UnityEngine;

namespace GameFeatures.ClickerFeature
{
    public class CodeWriter
    {
        private readonly TextWriterView _textView;
        private readonly string[] _textLines;
        private int _currentTextLine;
        
        public CodeWriter(IRepository<ScriptableObject> dataRepository, Clicker clicker, TextWriterView textView)
        {
            _textView = textView;
            
            CodeWritingConfig config = dataRepository.GetItem<CodeWritingConfig>().FirstOrDefault();
            
            if(config == null)
                throw new NullReferenceException("No CodeWritingConfig found");
            
            _textLines = config.TextLines;
            clicker.OnClick += WriteNextLine;
        }

        private void WriteNextLine()
        {
            if (_currentTextLine >= _textLines.Length)
                _currentTextLine = 0;
            
            _textView.AddLine(_textLines[_currentTextLine]);
            
            
            _currentTextLine++;
        }
    }
}