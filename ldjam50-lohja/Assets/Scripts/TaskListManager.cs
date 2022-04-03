using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskListManager : MonoBehaviour
{
    public TaskList taskList;

    // Start is called before the first frame update
    void Awake()
    {
        taskList.Reset();
    }
}
