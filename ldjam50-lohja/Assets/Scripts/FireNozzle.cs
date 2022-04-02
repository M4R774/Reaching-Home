using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireNozzle : MonoBehaviour
{
    public World world;

    public float extinguishForce = 0.2f;

    private Collider2D collider;

    private List<Fire> collidedFires;
    private Timer extinguishTimer;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        extinguishTimer = gameObject.AddComponent<Timer>();
        extinguishTimer.duration = 0.5f;
        extinguishTimer.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (extinguishTimer.isFinished)
        {
            ExtinquishFires();
            extinguishTimer.StartTimer();
        }
    }

    private void ExtinquishFires()
    {
        foreach (Fire fire in collidedFires)
        {
            fire.Extinguish(extinguishForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collidedFires = world.GetAllFiresWithinBounds(collider.bounds);

        Debug.Log(collidedFires.Count);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        collidedFires = world.GetAllFiresWithinBounds(collider.bounds);

        Debug.Log(collidedFires.Count);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collidedFires = world.GetAllFiresWithinBounds(collider.bounds);

        Debug.Log(collidedFires.Count);
    }
}
