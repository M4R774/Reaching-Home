using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerTask : Task
{
    private void Break() 
    {
        healthy = false;
        this.GetComponent<Animator>().SetTrigger("ChaosBreaks");
    }

    private void Fix() 
    {
        healthy = true;
        this.GetComponent<Animator>().SetTrigger("PlayerFixes");
    }

    public bool IsBroken() 
    {
        return healthy;
    }

    new public Vector3 GetPosition() 
    {
        return transform.position;
    }

    new public float GetUrgencyLevel()
    {
        throw new System.NotImplementedException();
    }


    // This interaction changes the sprite from 'broken' to 'healthy'
    // and is called only by the player
    new public void Interact() 
    {
        Debug.Log("Computer clicked!");
        Fix();
    }

    // This interaction changes the sprite from 'healthy' to 'broken'
    // and is called only by the game controller
    new public void ChaosInteract() 
    {
        Break();
    }

}
