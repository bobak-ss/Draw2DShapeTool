using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DrawShapeTool
{  
    public class DrawShape : MonoBehaviour
    {
        public int segmentsNum;
        public float segmentT;
        public float radius;

        public void Draw()
        {
            Vector3[] linePoints = DrawShapeStatic.SetUpPoints(segmentsNum, segmentT, radius);
            GameObject newGo = new GameObject();
            LineRenderer newGoLr = newGo.AddComponent<LineRenderer>();
            newGoLr.positionCount = linePoints.Length;
            newGoLr.SetPositions(linePoints);
            newGoLr.useWorldSpace = false;
            newGoLr.startWidth = 0.1f;
            newGoLr.endWidth = 0.1f;
            newGoLr.loop = true;
            Material newMaterial = new Material(Shader.Find("Sprites/Default"));
            newGoLr.name = "New Shape";
        }
    }
}
