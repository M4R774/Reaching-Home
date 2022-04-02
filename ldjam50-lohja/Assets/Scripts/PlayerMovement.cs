using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public GameObject fireNozzle;

    public float speed;

    private Rigidbody2D myRigidBody;

    public void Start()
    {
        myRigidBody = this.GetComponent<Rigidbody2D>();
        fireNozzle.SetActive(false);
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        Vector2 movementDirection = context.ReadValue<Vector2>();
        movementDirection.Normalize();
        
        if ( movementDirection.magnitude == 0.0f ) {
            GetComponent<Animator>().SetBool("Walking", false);
        } else {
            GetComponent<Animator>().SetBool("Walking", true);
        }

        myRigidBody.velocity = speed * movementDirection;

        Vector2 moveDirection = myRigidBody.velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void ToggleNozzle(InputAction.CallbackContext context)
    {
        bool on = context.ReadValue<float>() != 0;
        fireNozzle.SetActive(on);
    }
}
