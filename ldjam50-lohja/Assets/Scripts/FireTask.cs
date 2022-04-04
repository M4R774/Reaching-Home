using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTask : Task
{
    public GameObject player;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 2.5f)
        {
            Destroy(gameObject, 1f);
        }
    }

    public override void ChaosInteract()
    {
        throw new System.NotImplementedException();
    }

    public override void Interact()
    {
        throw new System.NotImplementedException();
    }

    public override Color GetTaskColor()
    {
        return Color.red;
    }
}
