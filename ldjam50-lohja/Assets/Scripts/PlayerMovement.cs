using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private Rigidbody2D myRigidBody;

    public void Start()
    {
        myRigidBody = this.GetComponent<Rigidbody2D>();
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        Vector2 movementDirection = context.ReadValue<Vector2>();
        myRigidBody.velocity = speed * movementDirection;
    }
}
