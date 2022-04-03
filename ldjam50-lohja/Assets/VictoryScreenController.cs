using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreenController : MonoBehaviour
{
    public float RotateSpeed = 5f;
    public float Radius = 2f;

    public Vector2 _centre = new Vector2(0,0);
    private float _angle = 4.7123889804f; // 270 astetta

    private float landingTime;

    private void Update()
    {

        _angle += RotateSpeed * Time.deltaTime; 

        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        transform.position = _centre + offset;

        transform.localScale -= new Vector3(.1f, .1f, .1f) * Time.deltaTime;

        if (transform.localScale.x < 0 && gameObject.GetComponent<SpriteRenderer>().enabled)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            landingTime = Time.time + 1f;
        }

        if (Time.time > landingTime && transform.localScale.x < 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
