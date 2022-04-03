using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public GameObject player;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        Vector3 offset = new Vector3(rb.velocity.x / 2, rb.velocity.y / 2, -1);
        float distance = Vector3.Distance(transform.position, player.transform.position + offset);
        float step = speed * Time.deltaTime * distance * distance; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position + offset, step);
    }
}
