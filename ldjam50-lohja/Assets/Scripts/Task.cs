using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task : MonoBehaviour
{
    public TaskList taskList;
    public bool healthy = true;
    public bool interactable = true;

    public void Awake()
    {
        taskList.AddTask(this.gameObject);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public float GetUrgencyLevel()
    {
        return 1.0f;
    }

    public abstract void Interact();

    public abstract void ChaosInteract();

    private void OnDestroy()
    {
        taskList.RemoveTask(gameObject);
    }
}
