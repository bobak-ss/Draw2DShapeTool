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
        public LineRenderer lineRenderer = null;

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
            
            if (lineRenderer == null)
            {
                lineRenderer = gameObject.AddComponent<LineRenderer>();
                lineRenderer.startWidth = 0.1f;
                lineRenderer.useWorldSpace = false;
                lineRenderer.endWidth = 0.1f;
                lineRenderer.loop = true;
            }
            lineRenderer.positionCount = newPoints.Length;
            lineRenderer.SetPositions(newPoints);
        }
    }
}
