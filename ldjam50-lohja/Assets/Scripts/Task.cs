using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour, ITask
{
    public TaskList taskList;
    public bool healthy = true;

    public void Start()
    {
        taskList.AddTask(this.gameObject);
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

    private void OnDestroy()
    {
        taskList.RemoveTask(gameObject);
    }
}
