using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LifeSupportTask : Task
{
    private Light2D omaValo;
    public Sprite healthySprite;
    public Sprite unhealthySprite;

    // Start is called before the first frame update
    void Start()
    {
        omaValo = gameObject.GetComponent<Light2D>();
    }

    // This interaction changes the sprite from 'broken' to 'healthy'
    // and is called only by the player
    override public void Interact()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = healthySprite;
        healthy = true;
        omaValo.color = Color.green;
    }


    // This interaction changes the sprite from 'healthy' to 'broken'
    // and is called only by the game controller
    override public void ChaosInteract()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = unhealthySprite;
        healthy = false;
        omaValo.color = Color.red;
    }
}
