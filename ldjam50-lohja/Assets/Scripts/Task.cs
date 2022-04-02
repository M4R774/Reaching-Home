using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour, ITask
{
    public TaskList taskList;

    public void Start()
    {
        taskList.tasks.Add(this.gameObject);
    }

    public Vector3 GetPosition()
    {
        throw new System.NotImplementedException();
    }

    public float GetUrgencyLevel()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public void ChaosInteract()
    {
        throw new System.NotImplementedException();
    }

}
