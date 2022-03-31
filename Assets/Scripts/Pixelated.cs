using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Pixelated : MonoBehaviour
{
    public RenderTexture render_texture;
    public int vertical_resolution;
    private int old_width;
    private int old_height;

    void Update()
    {
        if (old_width != Screen.width || old_height != Screen.height || vertical_resolution != render_texture.height)
        {
            float aspect_ratio = (float)Screen.width / (float)Screen.height;
            this.GetComponent<Camera>().targetTexture.Release();
            render_texture.height = vertical_resolution;
            render_texture.width = (int)(render_texture.height * aspect_ratio);
            this.GetComponent<Camera>().targetTexture = render_texture;
            this.GetComponent<Camera>().ResetAspect();
            old_width = Screen.width;
            old_height = Screen.height;
        }
    }
}
