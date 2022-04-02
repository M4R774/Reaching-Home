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

    // Update is called once per frame
    IEnumerator UpdateFireLights()
    {
        while (true)
        {
            foreach (Fire fire in world.activeFires)
            {
                Vector3 firePosition = new Vector3();
                Collider2D[] collisions = Physics2D.OverlapCircleAll(firePosition, 3);
                foreach (Collider2D collision in collisions)
                {

                }
                yield return new WaitForSeconds(.032f); // 0.032 = 30fps
            }
        }
    }
}
