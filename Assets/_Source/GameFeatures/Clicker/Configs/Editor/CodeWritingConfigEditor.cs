using UnityEditor;
using UnityEngine;

namespace GameFeatures.Clicker
{
    [CustomEditor(typeof(CodeWritingConfig))]
    public class CodeWritingConfigEditor : UnityEditor.Editor
    {
        private SerializedProperty _textAsset;
        private SerializedProperty _textLines;
        
        public void OnEnable()
        {
            _textAsset = serializedObject.FindProperty("_textAsset");
            _textLines = serializedObject.FindProperty("TextLines");
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.PropertyField(_textAsset);
            
            EditorGUILayout.Space(5);
            if (GUILayout.Button("Apply text asset"))
            {
                ((CodeWritingConfig)serializedObject.targetObject).ApplyTextAsset();
            }
            EditorGUILayout.Space(5);
            EditorGUILayout.PropertyField(_textLines);
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}