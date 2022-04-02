using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerTask : Task, ITask
{

    bool healthy = true;

    private void Break() 
    {
        healthy = false;
        // Do the necessary sprite change
    }

    private void Fix() 
    {
        healthy = true;
        // Do the necessary sprite change
    }

    public bool IsBroken() 
    {
        return healthy;
    }

    public Vector3 GetPosition() 
    {
        return transform.position;
    }

    public float GetUrgencyLevel()
    {
        throw new System.NotImplementedException();
    }


    // This interaction changes the sprite from 'broken' to 'healthy'
    // and is called only by the player
    public void Interact() 
    {
        Fix();
    }

    // This interaction changes the sprite from 'healthy' to 'broken'
    // and is called only by the game controller
    public void ChaosInteract() 
    {
        Break();
    }

}
