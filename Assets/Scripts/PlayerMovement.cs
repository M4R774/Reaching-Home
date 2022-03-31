using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody myRigidbody; //rigidBody gives the player physics 
    private Vector3 change; // the value we will use to tell unity where we moved 

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>(); //add the rigidbody component to this script for use later. 
    }


    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;

        change.x = Input.GetAxisRaw("Horizontal");
        change.z = Input.GetAxisRaw("Vertical");

        if (change != Vector3.zero)
        {
            MoveCharacter();
        }
    }

    void MoveCharacter()
    {
        //transform.position is the player position
        // add the "change" (x and y modifications) 
        //Time.Delta is the amount of time that has passed since the previous frame 
        //So what we are saying here is: Move my character TO my current poistion + the changes I asked to make (direction) * my current speed * the amount of time that has passed.
        //this last piece about the time change is to make it look more smooth when your character moves. 
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }
}
