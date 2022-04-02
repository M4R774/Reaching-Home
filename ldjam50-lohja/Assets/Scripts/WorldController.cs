using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldController : MonoBehaviour
{
    public World world;

    public Tilemap groundMap;
    public Tilemap terrainMap;
    public Tilemap objectMap;
    public Tilemap fireMap;

    private Timer fireTimer;

    // Start is called before the first frame update
    void Start()
    {
        world.InitMaps(groundMap, terrainMap, objectMap, fireMap);
        world.InitFires();
        fireTimer = gameObject.AddComponent<Timer>();
        fireTimer.duration = 0.5f;
        fireTimer.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer.isFinished)
        {
            world.TickFire();
            fireTimer.StopTimer();
        }
    }
}
