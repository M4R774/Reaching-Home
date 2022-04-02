using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldController : MonoBehaviour
{
    public World worldData;

    private Timer fireTimer;

    // Start is called before the first frame update
    void Start()
    {
        worldData.Init();
        fireTimer = gameObject.AddComponent<Timer>();
        fireTimer.duration = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer.isFinished)
        {
            worldData.TickFire();
        }
    }
}
