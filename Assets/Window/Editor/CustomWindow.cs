using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Window.Editor
{
    public class CustomWindow : EditorWindow
    {
        [MenuItem("Window/Custom")]
        public static void Get() => GetWindow<CustomWindow>("Custom Window");

        public void Update() => Repaint();

        public void OnGUI()
        {
            GL.Viewport(new Rect(0, 0, position.width, position.height));
            GL.Begin(GL.LINES);

            var orange = new Color(1, 0.5f, 0);
            
            _drawGradient(_getY(0), _getY(1), Color.red, orange);
            _drawGradient(_getY(1), _getY(2), orange, Color.yellow);
            _drawGradient(_getY(2), _getY(3), Color.yellow, Color.green);
            _drawGradient(_getY(3), _getY(4), Color.green, Color.blue);
            _drawGradient(_getY(4), _getY(5), Color.blue, Color.magenta);
            _drawGradient(_getY(5), _getY(6), Color.magenta, Color.red);

            GL.End();
        }

        private float _getY(int index) =>
            (position.height / 6f * index + (float) EditorApplication.timeSinceStartup * 100) % position.height * 2 - position.height / 2;
        
        private void _drawLine(Color c, float y)
        {
            GL.Color(c);
            GL.Vertex3(0, y, 0);
            GL.Vertex3(position.width, y, 0);
        }

        private void _drawGradient(float y1, float y2, Color c1, Color c2)
        {
            for (var y = y1; y < y2; y++)
            {
                var percentage = (y - y1) / (y2 - y1);
                var color = Color.Lerp(c1, c2, percentage);
                _drawLine(color, y);
            }
        }
    }
}
