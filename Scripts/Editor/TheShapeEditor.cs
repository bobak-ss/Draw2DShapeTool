using UnityEditor;
using UnityEngine;

namespace DrawShapeTool
{
    [CustomEditor(typeof(TheShape))]
    public class TheShapeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            TheShape theShape = (TheShape)target;

            using (var check = new EditorGUI.ChangeCheckScope())
            {
                theShape.segmentsNum = EditorGUILayout.IntSlider ("Segments Num", theShape.segmentsNum, 1, 10);
                theShape.segmentT = EditorGUILayout.Slider("Segments T", theShape.segmentT, -10, 10);
                theShape.radius = EditorGUILayout.Slider("Radius", theShape.radius, 1, 10);
                
                if (check.changed)
                {
                    Debug.Log("Changed!!");
                    theShape.ChangeTheShape();
                }
            }

            if (GUILayout.Button((theShape.lineRenderer != null) ? "Apply Changes" : "Create New Shape"))
            {
                theShape.ChangeTheShape();
            }

        }
        private void OnInspectorUpdate() 
        {
            Debug.Log("OnInspectorUpdate");
            TheShape theShape = (TheShape)target;
            theShape.ChangeTheShape();
        }
    }
}
