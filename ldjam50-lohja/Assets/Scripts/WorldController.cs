using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldController : MonoBehaviour
{
    public World world;

    public float eventTickDuration = 10.0f;

    public Tilemap groundMap;
    public Tilemap terrainMap;
    public Tilemap objectMap;
    public Tilemap fireMap;

    public List<AudioClip> textToSpeechClips;

    private Timer fireTimer;
    private Timer eventTimer;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        world.InitMaps(groundMap, terrainMap, objectMap, fireMap);
        world.InitFires();
        fireTimer = gameObject.AddComponent<Timer>();
        fireTimer.duration = 0.5f;
        fireTimer.StartTimer();

        eventTimer = gameObject.AddComponent<Timer>();
        eventTimer.duration = eventTickDuration;
        eventTimer.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer.isFinished)
        {
            world.TickFire();
            fireTimer.StartTimer();
        }

        if (eventTimer.isFinished)
        {
            TickEvents();
            eventTimer.StartTimer();
        }
    }

    void TickEvents()
    {
        world.StartFireAtRandomLocation();
        audioSource.clip = textToSpeechClips[0];
        audioSource.Play();
    }
}
