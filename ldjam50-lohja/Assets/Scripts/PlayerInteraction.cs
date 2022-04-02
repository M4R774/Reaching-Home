using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    
    // Keep a list of the closest items to interact with
    public List<Collider2D> collidersNearby = new List<Collider2D>();

    public void OnTriggerEnter2D(Collider2D other)
    {
        collidersNearby.Add(other);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        collidersNearby.Remove(other);
    }

    public void PlayerInteract() {
        if ( !(collidersNearby.Count == 0) ) {
            GetNearestCollider().gameObject.GetComponent<ITask>().Interact();
        }
    }


    private Collider2D GetNearestCollider() {

        Collider2D nearest = collidersNearby[0];
        
        float minDistance = float.MaxValue;
        
        foreach ( Collider2D collider in collidersNearby ) {
            Transform collTransform = collider.GetComponent<Transform>();
            float distance = Vector3.Distance(transform.position, collTransform.position);
            if ( distance < minDistance ) {
                nearest = collider;
                minDistance = distance;
            }
        }
        
        return nearest;
        
    }
}