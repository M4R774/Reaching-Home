using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ComputerTask : Task
{
    public World world;
    public Light2D lightSource;

    public void Start()
    {
        lightSource = gameObject.GetComponent<Light2D>();
        world.computers.Add(this);
    }
    
    private void Break() 
    {
        healthy = false;
        lightSource.color = Color.red;
        this.GetComponent<Animator>().SetTrigger("ChaosBreaks");
    }

    private void Fix() 
    {
        healthy = true;
        lightSource.color = Color.green;
        this.GetComponent<Animator>().SetTrigger("PlayerFixes");
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
        return Color.yellow;
    }
}
