using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour, ITask
{
    public ScriptableObject taskList;

    public float GetPosition()
    {
        throw new System.NotImplementedException();
    }

    public float GetUrgencyLevel()
    {
        throw new System.NotImplementedException();
    }
}
