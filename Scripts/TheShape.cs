using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DrawShapeTool
{
    public class TheShape : MonoBehaviour
    {
        
        public int segmentsNum;
        public float segmentT;
        public float radius;
        public LineRenderer lineRenderer;

        public void Initialize(int _segmentsNum, float _segmentT, float _radius, LineRenderer _lineRenderer)
        {
            segmentsNum = _segmentsNum;
            segmentT = _segmentT;
            radius = _radius;
            lineRenderer = _lineRenderer;
        }

        public void ChangeTheShape()
        {
            Vector3[] newPoints = DrawShapeStatic.SetUpPoints(segmentsNum, segmentT, radius);
            lineRenderer.positionCount = newPoints.Length;
            lineRenderer.SetPositions(newPoints);
        }
    }
}
