using UnityEditor;
using UnityEngine;

namespace DrawShapeTool
{
    [CustomEditor(typeof(DrawShape))]
    public class DrawShapeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawShape drawShape = (DrawShape)target;

            drawShape.segmentsNum = EditorGUILayout.IntField(new GUIContent("Segments Num"), drawShape.segmentsNum);
            drawShape.segmentT = EditorGUILayout.FloatField(new GUIContent("Segments T"), drawShape.segmentT);
            drawShape.radius = EditorGUILayout.FloatField(new GUIContent("Radius"), drawShape.radius);

            if(GUILayout.Button("Draw"))
            {
                drawShape.Draw();
            }
        }
    }
}
