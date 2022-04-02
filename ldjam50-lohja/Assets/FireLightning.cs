using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLightning : MonoBehaviour
{
    public World world;
    public GameObject FireLight;
    public GameObject player;

    public void Start()
    {
        StartCoroutine(UpdateFireLights());
    }

    IEnumerator UpdateFireLights()
    {
        while (true)
        {
            for (int i = 0; i < world.activeFires.Count; i++)
            {
                Vector3 firePosition = world.activeFires[i].GetWorldPosition();
                Collider2D[] collisions = Physics2D.OverlapCircleAll(firePosition, 3);
                int firesInRange = 0;
                foreach (Collider2D collision in collisions)
                {
                    if (collision.gameObject.tag == "FireLight")
                    {
                        firesInRange++;
                        break;
                    }
                }
                if (firesInRange < 1)
                {
                    GameObject newFireLight = Instantiate(FireLight, transform);
                    newFireLight.transform.position = world.activeFires[i].GetWorldPosition();
                }
                yield return new WaitForSeconds(.016f); // 0.032f = 30fps
            }
        }
    }
}
