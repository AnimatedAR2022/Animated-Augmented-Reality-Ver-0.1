using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineAnimator : MonoBehaviour
{
    [SerializeField] private float animationDuration = 5f;

    public List<Vector3> laserMode1 = new List<Vector3>();
    public List<Vector3> laserMode2 = new List<Vector3>();
    public List<Vector3> laserMode3 = new List<Vector3>();

    public int laserMode = 1;

    private LineRenderer lineRenderer;
    private Vector3[] linePoints;
    private int pointsCount;
    private int currentMode;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Store a copy of lineRenderer's points in linePoints array
        if (laserMode == 3)
        {
            lineRenderer.positionCount = 3;
            pointsCount = 3;
        }
        else
        {
            lineRenderer.positionCount = 2;
            pointsCount = 2;
        }
        animationDuration = 2f;
        if (laserMode != 1)
        {
            animationDuration = 0.0f;
            lineRenderer.SetPosition(0, laserMode2[0]);
            lineRenderer.SetPosition(1, laserMode2[1]);
        }
        linePoints = new Vector3[pointsCount];
        for (int i = 0; i < pointsCount; i++)
        {
            if (laserMode == 1)
                linePoints[i] = laserMode1[i];
            else if (laserMode == 2)
                linePoints[i] = laserMode2[i];
            else if (laserMode == 3)
                linePoints[i] = laserMode3[i];
        }
        currentMode = laserMode;
        StartCoroutine(AnimateLine());
    }

    private void Update()
    {
        if (currentMode != laserMode)
        {
            if (laserMode == 3)
            {
                lineRenderer.positionCount = 3;
                pointsCount = 3;
            }
            else
            {
                lineRenderer.positionCount = 2;
                pointsCount = 2;
            }
            animationDuration = 2f;
            if (laserMode != 1)
            {
                animationDuration = 0.0f;
                lineRenderer.SetPosition(0, laserMode2[0]);
                lineRenderer.SetPosition(1, laserMode2[1]);
            }
            linePoints = new Vector3[pointsCount];
            for (int i = 0; i < pointsCount; i++)
            {
                if (laserMode == 1)
                    linePoints[i] = laserMode1[i];
                else if (laserMode == 2)
                    linePoints[i] = laserMode2[i];
                else if (laserMode == 3)
                    linePoints[i] = laserMode3[i];
            }
        }
        currentMode = laserMode;
        StartCoroutine(AnimateLine());
    }

    private IEnumerator AnimateLine()
    {
        float segmentDuration = animationDuration / pointsCount;

        for (int i = 0; i < pointsCount; i++)
        {
            float startTime = Time.time;

            Vector3 startPosition = linePoints[i];
            Vector3 endPosition = linePoints[i];

            Vector3 pos = startPosition;
            while (pos != endPosition)
            {
                float t = (Time.time - startTime) / segmentDuration;
                pos = Vector3.Lerp(startPosition, endPosition, t);

                // animate all other points except point at index i
                for (int j = i + 1; j < pointsCount; j++)
                    lineRenderer.SetPosition(j, pos);

                yield return null;
            }
        }
    }
}