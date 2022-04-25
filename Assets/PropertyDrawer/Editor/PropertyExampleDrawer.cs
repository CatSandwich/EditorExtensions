using UnityEditor;
using UnityEngine;

namespace PropertyDrawer.Editor
{
    [CustomPropertyDrawer(typeof(PropertyExample))]
    public class PropertyExampleDrawer : UnityEditor.PropertyDrawer
    {
        private const float Margin = 1f;
        
        // Override the height of the property, creating space to draw the additional elements
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUIUtility.singleLineHeight * 2 + Margin + 5;
        
        // Override the default inspector
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            // Do some math to calculate screen space for elements
            var r1 = new Rect(rect) {height = EditorGUIUtility.singleLineHeight};
            var r2 = new Rect(r1) {y = rect.y + EditorGUIUtility.singleLineHeight + Margin};

            // Grab the Rotation property of the PropertyExample SerializedProperty
            var rotation = property.FindPropertyRelative("Rotation");
            
            // Draw the inspector for Rotation
            EditorGUI.PropertyField(r1, rotation, new GUIContent($"{fieldInfo.Name}'s rotation"));
            // Draw the button to reset to default
            if (GUI.Button(r2, "Reset to default"))
            {
                rotation.vector3Value = new Vector3();
            }
        }
    }
}
