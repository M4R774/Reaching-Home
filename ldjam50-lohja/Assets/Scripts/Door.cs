using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Door : Task
{
    public bool opened = false;
    private void Open()
    {
        opened = true;
        this.GetComponent<Animator>().SetTrigger("DoorOpen");
    }

    private void Close()
    {
        opened = false;
        this.GetComponent<Animator>().SetTrigger("DoorClose");
    }
    override public void Interact()
    {
       if (opened) {
           Close();
       } else {
           Open();
       }
    }


    override public void ChaosInteract()
    {
        throw new System.NotImplementedException();
    }
}
