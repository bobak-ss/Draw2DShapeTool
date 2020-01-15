using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DrawShapeTool
{
    public class DrawShape : MonoBehaviour
    {
        //public float speed = 10f;
        public int SegmentsNum = 4;
        public int MainCircleRadius = 2;
        public float _SegmentsT = 0;
        public Text inputTxt;

        private const int CirclePointsNum = 360;
        private Circle mainCircle = new Circle();
        private Vector3[] _segmentsPoints;
        private Vector3[] _segmentsDefaultCenterPoints;
        private Vector3[] _segmentsCenterPoints;
        //private Vector3 myEuler = new Vector3(0, 0, 1);
        private LineRenderer lr;

        private int frame = 10;
        // Use this for initialization
        void Start()
        {
            lr = GetComponent<LineRenderer>();
            mainCircle.center = new Vector3(0, 0, 0);
            mainCircle.r = MainCircleRadius;
            
            SetUpPoints(SegmentsNum);
            // SetUpLine(_segmentsPoints, _segmentsCenterPoints);
            //myEuler = new Vector3(0, 0, speed);
        }

        // Update is called once per frame
        void Update()
        {
            frame--;
            if (frame < 0)
            {
                // ReShape();

                frame = 10;
            }
        }

        public Vector3[] SetUpPoints(int n)
        {
            // setting up segment points
            _segmentsPoints = new Vector3[n];
            for (int i = 0; i < _segmentsPoints.Length; i++)
            {
                _segmentsPoints[i] = new Vector3(rCos(MainCircleRadius, i * (360f / n)), rSin(MainCircleRadius, i * (360f / n)), 0);
            }

            // setting up segment's default center points
            _segmentsDefaultCenterPoints = new Vector3[n];
            for (int i = 0; i < _segmentsDefaultCenterPoints.Length; i++)
            {
                if (i == _segmentsDefaultCenterPoints.Length - 1)
                    _segmentsDefaultCenterPoints[i] = new Vector3((_segmentsPoints[i].x + _segmentsPoints[0].x) / 2, (_segmentsPoints[i].y + _segmentsPoints[0].y) / 2, 0);
                else
                    _segmentsDefaultCenterPoints[i] = new Vector3((_segmentsPoints[i].x + _segmentsPoints[i + 1].x) / 2, (_segmentsPoints[i].y + _segmentsPoints[i + 1].y) / 2, 0);
            }
            _segmentsCenterPoints = _segmentsDefaultCenterPoints;

            float a = 0f;
            for (int i = 0; i < _segmentsCenterPoints.Length; i++)
            {
                a = (180 / n) + (i * (360 / n));
                _segmentsCenterPoints[i] = _segmentsCenterPoints[i] + new Vector3(rCos(_SegmentsT, a), rSin(_SegmentsT, a), 0);
            }
            // _segmentsCenterPoints = SetUpSegmentsCenterPoints(_segmentsDefaultCenterPoints, _SegmentsT, n);

            // setup line
            int pointsCount = _segmentsPoints.Length + _segmentsCenterPoints.Length;
            Vector3[] mainPoints = new Vector3[pointsCount];
            for (int i = 0, j = 0; i < mainPoints.Length; i += 2, j++)
            {
                mainPoints[i] = _segmentsPoints[j];
                mainPoints[i + 1] = _segmentsCenterPoints[j];
            }

            Vector3[] points = new Vector3[CirclePointsNum];
            int eachSegSidePointCount = CirclePointsNum / (2 * SegmentsNum);
            int segmentIndex = 0;
            int s = 0;
            Vector3 tmp = Vector3.zero;
            while(s < CirclePointsNum - 1)
            {
                Debug.Log(segmentIndex);
                Debug.Log(s);
                tmp = _segmentsPoints[segmentIndex];
                points[s] = tmp;
                for (int i = 1; i < eachSegSidePointCount; i++)
                {
                    points[s + i] = new Vector3( ((_segmentsCenterPoints[segmentIndex].x - _segmentsPoints[segmentIndex].x) / eachSegSidePointCount) * i + _segmentsPoints[segmentIndex].x,
                                                    ((_segmentsCenterPoints[segmentIndex].y - _segmentsPoints[segmentIndex].y) / eachSegSidePointCount) * i + _segmentsPoints[segmentIndex].y,
                                                    0);
                }
                s += eachSegSidePointCount;
                points[s] = _segmentsCenterPoints[segmentIndex];
                for (int i = 1; i < eachSegSidePointCount; i++)
                {
                    points[s + i] = new Vector3( ((_segmentsCenterPoints[segmentIndex].x - _segmentsPoints[segmentIndex].x) / eachSegSidePointCount) * i + _segmentsPoints[segmentIndex].x,
                                                    ((_segmentsCenterPoints[segmentIndex].y - _segmentsPoints[segmentIndex].y) / eachSegSidePointCount) * i + _segmentsPoints[segmentIndex].y,
                                                    0);
                }
                s += eachSegSidePointCount;
                segmentIndex++;
            }

            // lr.positionCount = mainPoints.Length;
            // lr.SetPositions(mainPoints);
            // lr.useWorldSpace = false;
            return mainPoints;
        }

        public Vector3[] SetUpSegmentsCenterPoints(Vector3[] segmentsDefaultCP, float t, int n)
        {
            Vector3[] SegmentsCenterPoints = new Vector3[n];
            float a = 0f;
            for (int i = 0; i < SegmentsCenterPoints.Length; i++)
            {
                a = (180 / n) + (i * (360 / n));
                SegmentsCenterPoints[i] = segmentsDefaultCP[i] + new Vector3(rCos(t, a), rSin(t, a), 0);
            }
            return SegmentsCenterPoints;
        }

        public Vector3[] SetUpLine(Vector3[] segmentPs = null, Vector3[] centerPs = null)
        {
            int pointsCount = segmentPs.Length + centerPs.Length;
            Vector3[] mainPoints = new Vector3[pointsCount];
            for (int i = 0, j = 0; i < mainPoints.Length; i += 2, j++)
            {
                mainPoints[i] = segmentPs[j];
                mainPoints[i + 1] = centerPs[j];
            }

            Vector3[] points = new Vector3[CirclePointsNum];
            int eachSegSidePointCount = CirclePointsNum / (2 * SegmentsNum);
            int segmentIndex = 0;
            int s = 0;
            Vector3 tmp = Vector3.zero;
            while(s < CirclePointsNum - 1)
            {
                Debug.Log(segmentIndex);
                Debug.Log(s);
                tmp = segmentPs[segmentIndex];
                points[s] = tmp;
                for (int i = 1; i < eachSegSidePointCount; i++)
                {
                    points[s + i] = new Vector3( ((centerPs[segmentIndex].x - segmentPs[segmentIndex].x) / eachSegSidePointCount) * i + segmentPs[segmentIndex].x,
                                                    ((centerPs[segmentIndex].y - segmentPs[segmentIndex].y) / eachSegSidePointCount) * i + segmentPs[segmentIndex].y,
                                                    0);
                }
                s += eachSegSidePointCount;
                points[s] = centerPs[segmentIndex];
                for (int i = 1; i < eachSegSidePointCount; i++)
                {
                    points[s + i] = new Vector3( ((centerPs[segmentIndex].x - segmentPs[segmentIndex].x) / eachSegSidePointCount) * i + segmentPs[segmentIndex].x,
                                                    ((centerPs[segmentIndex].y - segmentPs[segmentIndex].y) / eachSegSidePointCount) * i + segmentPs[segmentIndex].y,
                                                    0);
                }
                s += eachSegSidePointCount;
                segmentIndex++;
            }

            // lr.positionCount = mainPoints.Length;
            // lr.SetPositions(mainPoints);
            // lr.useWorldSpace = false;

            return mainPoints;
        }

        public float rCos(float r, float angle)
        {
            return r * Mathf.Cos(angelToRadian(angle));
        }
        public float rSin(float r, float angle)
        {
            return r * Mathf.Sin(angelToRadian(angle));
        }

        public void printArray(Vector3[] a)
        {
            Debug.Log("///////////////////////\nPrintArray ");
            for (int i = 0; i < a.Length; i++)
            {
                Debug.Log(a[i]);
            }
        }

        public float angelToRadian(float angel)
        {
            return ((float)Math.PI * angel) / 180;
        }

        public void Draw()
        {
            Vector3[] linePoints = SetUpPoints(SegmentsNum);
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
