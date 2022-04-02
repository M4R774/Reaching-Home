using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class PointerToTask : MonoBehaviour
{
    public TaskList taskList;
    LineRenderer lr;

    public GameObject arrowPrefab;
    public List<GameObject> arrows;

    public float a = .5f;
    public float b = .5f;
    public int resolution = 1000;

    Vector2[] taskPointerArrowPointLocations;


    private void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void FixedUpdate()
    {
        // DrawDebugLinesAndEllipse();

        foreach (GameObject arrow in arrows)
        {
            Destroy(arrow);
        }
        arrows.Clear();

        foreach (GameObject task in taskList.tasks)
        {
            DrawTaskArrow(task);
        }
    }

    private void DrawDebugLinesAndEllipse()
    {
        DrawDebugEllipse();

        int counter = 0;
        foreach (GameObject task in taskList.tasks)
        {
            DrawDebugLineToTask(task, counter);
            counter += 2;
        }
    }

    void OnDrawGizmos()
    {
        if (taskPointerArrowPointLocations != null && taskPointerArrowPointLocations.Length > 0)
        {
            foreach (Vector2 intersectionPoint in taskPointerArrowPointLocations)
            {
                // Draw a yellow sphere at the transform's position
                Gizmos.color = Color.magenta;
                Gizmos.DrawSphere(new Vector3(intersectionPoint.x, intersectionPoint.y, 0), .1f);
            }
        }
    }

    private void DrawDebugLineToTask(GameObject task, int counter)
    {
        //lr.positionCount = taskList.tasks.Count * 2;
        lr.SetPosition(counter, this.transform.position);
        lr.SetPosition(counter+1, task.transform.position);
    }

    private void DrawDebugEllipse()
    {
        Vector3[] positions = CreateEllipse(a, b, resolution);
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.positionCount = resolution + 1;
        for (int i = 0; i <= resolution; i++)
        {
            lr.SetPosition(i, positions[i]);
        }
    }

    private void DrawTaskArrow(GameObject task)
    {
        Vector2 pt1 = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector2 pt2 = new Vector2(task.transform.position.x, task.transform.position.y);
        Vector2 rectPosition = new Vector2(this.transform.position.x - a, this.transform.position.y - b);
        Vector2 rectSize = new Vector2(a*2, b*2);
        Rect rect = new Rect(rectPosition, rectSize);
        taskPointerArrowPointLocations = CalculateIntersectionPoint(rect, pt1, pt2, true);
        if (taskPointerArrowPointLocations.Length > 0)
        {
            InstantiateTaskArrow(task, taskPointerArrowPointLocations[0]);
        }
    }

    private void InstantiateTaskArrow(GameObject task, Vector2 arrowLocation)
    {
        Vector3 spawnLocation = new Vector3(arrowLocation.x, arrowLocation.y, 0);
        GameObject arrow = Instantiate(arrowPrefab, this.transform);
        arrow.transform.position = spawnLocation;
        arrows.Add(arrow);


        float angle = Mathf.Atan2(task.transform.position.y - arrow.transform.position.y, task.transform.position.x - arrow.transform.position.x) * Mathf.Rad2Deg - 90;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        arrow.transform.rotation = targetRotation; // Quaternion.RotateTowards(arrow.transform.rotation, targetRotation, 1 * Time.deltaTime);
    }

    private Vector2[] CalculateIntersectionPoint(Rect rect, Vector2 pt1, Vector2 pt2, bool segment_only)
    {
        // Find the points of intersection between
        // an ellipse and a line segment.
        {
            // If the ellipse or line segment are empty, return no intersections.
            if ((rect.width == 0) || (rect.height == 0) ||
                ((pt1.x == pt2.x) && (pt1.y == pt2.y)))
                return new Vector2[] { };

            // Make sure the rectangle has non-negative width and height.
            if (rect.width < 0)
            {
                rect.x = rect.xMax;
                rect.width = -rect.width;
            }
            if (rect.height < 0)
            {
                rect.y = rect.yMax;
                rect.height = -rect.height;
            }

            // Translate so the ellipse is centered at the origin.
            float cx = rect.xMin + rect.width / 2f;
            float cy = rect.yMin + rect.height / 2f;
            rect.x -= cx;
            rect.y -= cy;
            pt1.x -= cx;
            pt1.y -= cy;
            pt2.x -= cx;
            pt2.y -= cy;

            // Get the semimajor and semiminor axes.
            float a = rect.width / 2;
            float b = rect.height / 2;

            // Calculate the quadratic parameters.
            float A = (pt2.x - pt1.x) * (pt2.x - pt1.x) / a / a +
                      (pt2.y - pt1.y) * (pt2.y - pt1.y) / b / b;
            float B = 2 * pt1.x * (pt2.x - pt1.x) / a / a +
                      2 * pt1.y * (pt2.y - pt1.y) / b / b;
            float C = pt1.x * pt1.x / a / a + pt1.y * pt1.y / b / b - 1;

            // Make a list of t values.
            List<float> t_values = new List<float>();

            // Calculate the discriminant.
            float discriminant = B * B - 4 * A * C;
            if (discriminant == 0)
            {
                // One real solution.
                t_values.Add(-B / 2 / A);
            }
            else if (discriminant > 0)
            {
                // Two real solutions.
                t_values.Add((float)((-B + Math.Sqrt(discriminant)) / 2 / A));
                t_values.Add((float)((-B - Math.Sqrt(discriminant)) / 2 / A));
            }

            // Convert the t values into points.
            List<Vector2> points = new List<Vector2>();
            foreach (float t in t_values)
            {
                // If the points are on the segment (or we
                // don't care if they are), add them to the list.
                if (!segment_only || ((t >= 0f) && (t <= 1f)))
                {
                    float x = pt1.x + (pt2.x - pt1.x) * t + cx;
                    float y = pt1.y + (pt2.y - pt1.y) * t + cy;
                    points.Add(new Vector2(x, y));
                }
            }

            // Return the points.
            return points.ToArray();
        }
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
