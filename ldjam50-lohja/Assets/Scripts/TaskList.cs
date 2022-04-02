using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TaskList : ScriptableObject
{
    public List<GameObject> tasks;

    public List<GameObject> GetTasksInUrgencyOrder()
    {
        // TODO
        return null;
    }
}
