using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Scaler : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float scalePerSecond = 0.1f;

    public float minScale = 1.0f;
    public float maxScale = 2.0f;

    private float direction = 1;

    // Update is called once per frame
    void Update()
    {
        float scaleFactor = scalePerSecond * Time.deltaTime;
        Vector3 currentScale = transform.localScale;
        float scale = scaleFactor * direction;
        transform.localScale = new Vector3(currentScale.x + currentScale.x * scale, currentScale.y + currentScale.y * scale, currentScale.z + currentScale.z * scale);

        if (transform.localScale.magnitude > maxScale)
        {
            direction = -1;
        } 
        else if (transform.localScale.magnitude < minScale)
        {
            direction = 1;
        }
    }
}
