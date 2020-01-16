using UnityEngine;

namespace DrawShapeTool
{  
    public class DrawNewShape : MonoBehaviour
    {
        public int segmentsNum;
        public float segmentT;
        public float radius;

        public void Draw()
        {
            GameObject newGo = new GameObject();
            TheShape theShape = newGo.AddComponent<TheShape>();
            LineRenderer newGoLr = newGo.AddComponent<LineRenderer>();
            theShape.Initialize(segmentsNum, segmentT, radius, newGoLr);
            Vector3[] points = DrawShapeStatic.SetUpPoints(segmentsNum, segmentT, radius);
            newGoLr.positionCount = points.Length;
            newGoLr.SetPositions(points);
            newGoLr.useWorldSpace = false;
            newGoLr.startWidth = 0.1f;
            newGoLr.endWidth = 0.1f;
            newGoLr.loop = true;
            Material newMaterial = new Material(Shader.Find("Sprites/Default"));
            newGoLr.name = "New Shape";

        }
    }
}
