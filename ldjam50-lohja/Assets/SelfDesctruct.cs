using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SelfDesctruct : MonoBehaviour
{
    public GameObject player;
    public Fire fire;
    private Light2D light2d;
    public Vector3 originalPosition;

    void Start()
    {
        light2d = gameObject.GetComponent<Light2D>();
        Destroy(gameObject, 3.5f);
    }

    private void FixedUpdate()
    {
        light2d.intensity = Mathf.Clamp(light2d.intensity + Random.Range(-0.03f, 0.03f), 0.7f, 0.9f);
        float x_flicker = Mathf.Clamp(
                (transform.position.x + Random.Range(-.03f, .03f)), 
                originalPosition.x - .1f, 
                originalPosition.x + .1f
            );
        float y_flicker = Mathf.Clamp(
                (transform.position.y + Random.Range(-.03f, .03f)),
                originalPosition.y - .1f,
                originalPosition.y + .1f
            );
        transform.position = new Vector3(x_flicker, y_flicker, 0);
    }
}
