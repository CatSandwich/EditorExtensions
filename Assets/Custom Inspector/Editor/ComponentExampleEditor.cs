using UnityEditor;
using UnityEngine;

namespace Custom_Inspector.Editor
{
    // This attribute tells unity which type this editor override the inspector behaviour for.
    [CustomEditor(typeof(ComponentExample))]
    public class ComponentExampleEditor : UnityEditor.Editor
    {
        Material Mat;
        
        private void OnEnable()
        {
            var shader = Shader.Find("Hidden/Internal-Colored");
            Mat = new Material(shader);
        }
        
        private void OnDisable()
        {
            DestroyImmediate(Mat);
        }
        
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("This is a label added through the custom editor.");
            EditorGUILayout.LabelField("Below is some GL fun.");
            
            var rect = GUILayoutUtility.GetRect(10, 1000, 200, 200);
            if (Event.current.type != EventType.Repaint) return;
            
            GUI.BeginClip(rect);
            GL.Clear(true, false, Color.black);
            Mat.SetPass(0);
         
            GL.Begin(GL.LINES);
            _drawLine(Color.red, new Vector3(0, 0), new Vector3(rect.width, rect.height));
            _drawLine(Color.blue, new Vector3(rect.width, 0), new Vector3(0, rect.height));
            _drawLine(Color.green, new Vector3(0, rect.height / 2), new Vector3(rect.width, rect.height / 2));
            _drawLine(Color.magenta, new Vector3(rect.width / 2, 0), new Vector3(rect.width / 2, rect.height));
            GL.End();
            GUI.EndClip();
        }

        private void _drawLine(Color color, Vector3 start, Vector3 end)
        {
            GL.Color(color);
            GL.Vertex(start);
            GL.Vertex(end);
        }
    }
}
