using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class WorldController : MonoBehaviour
{
    public World world;
    public GameObject player;
    public float eventTickDurationOnStart = 10.0f;

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
        world.InitEngines();
        world.InitComputers();
        fireTimer = gameObject.AddComponent<Timer>();
        fireTimer.duration = 0.5f;
        fireTimer.StartTimer();

        eventTimer = gameObject.AddComponent<Timer>();
        eventTimer.duration = eventTickDurationOnStart;
        eventTimer.StartTimer();
    }

    private void Start()
    {
        world.player = player;
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
            Debug.Log("event timer is finished");
            TickEvents();
            eventTimer.StartTimer();
        }
    }

    void TickEvents()
    {
        int lottery = Random.Range(1, 3);
        switch (lottery) 
        {
            case 1:
                // Computer breakdown   
                BreakRandomComputer();
                ComputerAudio();
                break;
            case 2:
                // Engine breakdown
                BreakRandomEngine();
                EngineAudio();
                break;
            default:
                // Fire
                world.StartFireAtRandomLocation();
                FireAudio();
                break;
        }
        
    }

    private void BreakRandomComputer()
    {
        if ( world.computers != null && world.computers.Count != 0 ) {
            world.computers[Random.Range(0, world.computers.Count)].ChaosInteract();
        }
    }

    private void BreakRandomEngine() 
    {
        if ( world.engines != null && world.engines.Count != 0 ) {
            world.engines[Random.Range(0, world.engines.Count)].ChaosInteract();
        }
    }


    private void FireAudio() 
    {
        audioSource.clip = textToSpeechClips[0];
        audioSource.Play();
    }

    private void ComputerAudio() 
    {
        audioSource.clip = textToSpeechClips[1];
        audioSource.Play();
    }

    private void EngineAudio() 
    {
        audioSource.clip = textToSpeechClips[2];
        audioSource.Play();
    }
}
