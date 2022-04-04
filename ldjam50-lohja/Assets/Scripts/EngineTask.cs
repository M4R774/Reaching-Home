using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EngineTask : Task
{

    public World world;
    
    public void Start()
    {
        world.engines.Add(this);
    }

    private void Break() 
    {
        healthy = false;
        this.GetComponent<Animator>().SetTrigger("ChaosBreaks");
        this.GetComponentInChildren<Light2D>().intensity = 0;
    }

    private void Fix() 
    {
        healthy = true;
        this.GetComponent<Animator>().SetTrigger("PlayerFixes");
        this.GetComponentInChildren<Light2D>().intensity = 0.5f;
    }

    public bool IsBroken() 
    {
        return healthy;
    }


    // This interaction changes the sprite from 'broken' to 'healthy'
    // and is called only by the player
    override public void Interact() 
    {
        Fix();
    }

    // This interaction changes the sprite from 'healthy' to 'broken'
    // and is called only by the game controller
    override public void ChaosInteract() 
    {
        Break();
    }

    public override Color GetTaskColor()
    {
        return Color.blue;
    }
}
