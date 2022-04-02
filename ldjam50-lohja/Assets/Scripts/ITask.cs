using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITask
{
    public float GetUrgencyLevel();
    public Vector3 GetPosition();

    public void Interact();

    public void ChaosInteract();
}
