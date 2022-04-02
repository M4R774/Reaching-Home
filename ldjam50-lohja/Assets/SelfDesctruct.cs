using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SelfDesctruct : MonoBehaviour
{
    public GameObject player;
    public Fire fire;
    private Light2D light2d;

    void Start()
    {
        light2d = gameObject.GetComponent<Light2D>();
        Destroy(gameObject, 5f);
    }

    private void FixedUpdate()
    {
        light2d.intensity = Mathf.Clamp(light2d.intensity + Random.Range(-0.02f, 0.02f), 0.7f, 0.9f);
    }
}
