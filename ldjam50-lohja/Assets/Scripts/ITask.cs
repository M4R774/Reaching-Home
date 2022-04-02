using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITask
{
    public float GetUrgencyLevel();
    public float GetPosition();
}
