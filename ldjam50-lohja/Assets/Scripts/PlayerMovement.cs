using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidBody;

    public void Start()
    {
        myRigidBody = this.GetComponent<Rigidbody2D>();
    }

    public void MovePlayer(Vector2 movementDirection)
    {
        myRigidBody.velocity = speed * movementDirection;
    }
}
