using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerTask : Task, ITask
{

    bool healthy = true;

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
        Debug.Log("Computer clicked!");
        Fix();
    }

    // This interaction changes the sprite from 'healthy' to 'broken'
    // and is called only by the game controller
    public void ChaosInteract() 
    {
        Break();
    }

}
