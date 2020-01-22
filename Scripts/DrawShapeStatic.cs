using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DrawShapeTool
{
    public static class DrawShapeStatic
    {
        public static int segmentsNum;
        public static float mainCircleRadius;
        public static float segmentsT;

        private const int circlePointsNum = 360;
        private static Vector3[] segmentsPoints;
        private static Vector3[] segmentsCenterPoints;

        public static Vector3[] SetUpPoints(int _segmentsNum, float _SegmentsT, float _mainCircleRadius)
        {
            segmentsNum = _segmentsNum;
            segmentsT = _SegmentsT;
            mainCircleRadius = _mainCircleRadius;

            // setting up segment points
            segmentsPoints = new Vector3[_segmentsNum];
            for (int i = 0; i < segmentsPoints.Length; i++)
            {
                segmentsPoints[i] = new Vector3(rCos(mainCircleRadius, i * (360f / _segmentsNum)), rSin(mainCircleRadius, i * (360f / _segmentsNum)), 0);
            }

            // setting up segment's default center points
            segmentsCenterPoints = new Vector3[_segmentsNum];
            for (int i = 0; i < segmentsCenterPoints.Length; i++)
            {
                if (i == segmentsCenterPoints.Length - 1)
                    segmentsCenterPoints[i] = new Vector3((segmentsPoints[i].x + segmentsPoints[0].x) / 2, (segmentsPoints[i].y + segmentsPoints[0].y) / 2, 0);
                else
                    segmentsCenterPoints[i] = new Vector3((segmentsPoints[i].x + segmentsPoints[i + 1].x) / 2, (segmentsPoints[i].y + segmentsPoints[i + 1].y) / 2, 0);
            }

            // apply segment T
            float alpha = 0f;
            for (int i = 0; i < segmentsCenterPoints.Length; i++)
            {
                alpha = (180 / _segmentsNum) + (i * (360 / _segmentsNum));
                segmentsCenterPoints[i] = segmentsCenterPoints[i] + new Vector3(rCos(_SegmentsT, alpha), rSin(_SegmentsT, alpha), 0);
            }

            // setup line
            int pointsCount = segmentsPoints.Length + segmentsCenterPoints.Length;
            Vector3[] mainPoints = new Vector3[pointsCount];
            for (int i = 0, j = 0; i < mainPoints.Length; i += 2, j++)
            {
                mainPoints[i] = segmentsPoints[j];
                mainPoints[i + 1] = segmentsCenterPoints[j];
            }

            // circular segments
            // Vector3[] points = new Vector3[circlePointsNum];
            // int eachSegSidePointCount = circlePointsNum / (2 * segmentsNum);
            // int segmentIndex = 0;
            // int s = 0;
            // Vector3 tmp = Vector3.zero;
            // while(s < circlePointsNum - 1)
            // {
            //     // Debug.Log(segmentIndex);
            //     // Debug.Log(s);
            //     tmp = segmentsPoints[segmentIndex];
            //     points[s] = tmp;
            //     for (int i = 1; i < eachSegSidePointCount; i++)
            //     {
            //         points[s + i] = new Vector3( ((segmentsCenterPoints[segmentIndex].x - segmentsPoints[segmentIndex].x) / eachSegSidePointCount) * i + segmentsPoints[segmentIndex].x,
            //                                         ((segmentsCenterPoints[segmentIndex].y - segmentsPoints[segmentIndex].y) / eachSegSidePointCount) * i + segmentsPoints[segmentIndex].y,
            //                                         0);
            //     }
            //     s += eachSegSidePointCount;
            //     points[s] = segmentsCenterPoints[segmentIndex];
            //     for (int i = 1; i < eachSegSidePointCount; i++)
            //     {
            //         points[s + i] = new Vector3( ((segmentsCenterPoints[segmentIndex].x - segmentsPoints[segmentIndex].x) / eachSegSidePointCount) * i + segmentsPoints[segmentIndex].x,
            //                                         ((segmentsCenterPoints[segmentIndex].y - segmentsPoints[segmentIndex].y) / eachSegSidePointCount) * i + segmentsPoints[segmentIndex].y,
            //                                         0);
            //     }
            //     s += eachSegSidePointCount;
            //     segmentIndex++;
            // }
            
            return mainPoints;
        }
        
        public static float rCos(float r, float angle)
        {
            return r * Mathf.Cos(angelToRadian(angle));
        }
        public static float rSin(float r, float angle)
        {
            return r * Mathf.Sin(angelToRadian(angle));
        }

        public static void printArray(Vector3[] a)
        {
            Debug.Log("///////////////////////\nPrintArray ");
            for (int i = 0; i < a.Length; i++)
            {
                Debug.Log(a[i]);
            }
        }

        public static float angelToRadian(float angel)
        {
            return ((float)Math.PI * angel) / 180;
        }
    }
}
