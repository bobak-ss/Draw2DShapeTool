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

            drawShape.SegmentsNum = EditorGUILayout.IntField(new GUIContent("Segments Num"), drawShape.SegmentsNum);
            drawShape._SegmentsT = EditorGUILayout.FloatField(new GUIContent("Segments T"), drawShape._SegmentsT);
            drawShape.MainCircleRadius = EditorGUILayout.IntField(new GUIContent("Radius"), drawShape.MainCircleRadius);

            if(GUILayout.Button("Draw"))
            {
                drawShape.Draw();
            }
        }
    }
}
