using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class WorldController : MonoBehaviour
{
    [Header("References")]
    public World world;
    public GameObject player;
    public Tilemap groundMap;
    public Tilemap terrainMap;
    public Tilemap objectMap;
    public Tilemap fireMap;

    public List<AudioClip> textToSpeechClips;

    [Header("Ticks")]
    public float eventTickSpeed = 10.0f;
    public float fireTickSpeed = 0.5f;
    public float damageTickSpeed = 1.0f;

    private Timer fireTimer;
    private Timer eventTimer;
    private Timer damageTimer;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        world.InitMaps(groundMap, terrainMap, objectMap, fireMap);
        world.InitFires();
        world.InitEngines();
        world.InitComputers();
        world.damage = 0;

        fireTimer = gameObject.AddComponent<Timer>();
        InitTimer(fireTimer, fireTickSpeed);
        eventTimer = gameObject.AddComponent<Timer>();
        InitTimer(eventTimer, eventTickSpeed);
        damageTimer = gameObject.AddComponent<Timer>();
        InitTimer(damageTimer, damageTickSpeed);
    }

    private void InitTimer(Timer timer, float duration)
    {
        timer.duration = duration;
        timer.StartTimer();
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
            TickEvents();
            eventTimer.StartTimer();
        }

        if (damageTimer.isFinished)
        {
            world.TickDamage();
            damageTimer.StartTimer();
        }
    }

    void TickEvents()
    {
        int lottery = Random.Range(1, 4);
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
            case 3:
                // Fire
                world.StartFireAtRandomLocation();
                FireAudio();
                break;
            default:
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
