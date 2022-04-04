using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Door : Task
{
    public bool opened = false;
    public bool processing = false;

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

    private void ToggleHitbox() {
        if (opened) {
            this.transform.Find("Hitbox").GetComponent<BoxCollider2D>().enabled = false;
        } else {
            this.transform.Find("Hitbox").GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    override public void Interact()
    {
       if (opened) {
           Close();
       } else {
           Open();
       }
       ToggleHitbox();
    }

    override public void ChaosInteract()
    {
        throw new System.NotImplementedException();
    }

    public override Color GetTaskColor()
    {
        return Color.white;
    }
}
