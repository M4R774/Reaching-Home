using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class PointerToTask : MonoBehaviour
{
    public TaskList taskList;


    public float a = .5f;
    public float b = .5f;
    public int resolution = 1000;

    void FixedUpdate()
    {
        DrawDebugEllipse();

        int counter = 0;
        foreach (GameObject task in taskList.tasks)
        {
            DrawDebugLineToTask(task, counter);
            DrawTaskArrow();


            counter++;
        }
    }

    private void DrawDebugLineToTask(GameObject task, int counter)
    {
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, this.transform.position);
        lr.SetPosition(1, task.transform.position);
    }

    private void DrawDebugEllipse()
    {
        Vector3[] positions = CreateEllipse(a, b, resolution);
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.SetVertexCount(resolution + 1);
        for (int i = 0; i <= resolution; i++)
        {
            lr.SetPosition(i, positions[i]);
        }
    }

    private void DrawTaskArrow()
    {
        
    }

    private Vector3[] CreateEllipse(float a, float b, int resolution)
    {
        Vector3[] positions = new Vector3[resolution + 1];

        for (int i = 0; i <= resolution; i++)
        {
            float angle = (float)i / (float)resolution * 2.0f * Mathf.PI;
            positions[i] = this.transform.position + new Vector3(a * Mathf.Cos(angle), b * Mathf.Sin(angle), 1.0f);
        }

        return positions;
    }
}
