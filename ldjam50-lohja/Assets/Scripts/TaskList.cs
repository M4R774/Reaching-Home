using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TaskList : ScriptableObject
{
    private List<GameObject> tasks;

    public List<GameObject> GetTasks()
    {
        // TODO
        List<GameObject> unhealthyTasks = new List<GameObject>();
        unhealthyTasks.Clear();
        foreach(GameObject task in tasks)
        {
            if (task.GetComponent<Task>().healthy == false)
            {
                unhealthyTasks.Add(task);
            }
        }
        return unhealthyTasks;
    }

    internal void Reset()
    {
        tasks.Clear();
    }

    public void AddTask(GameObject task)
    {
        tasks.Add(task);
    }

    public void RemoveTask(GameObject task)
    {
        tasks.Remove(task);
    }
}
