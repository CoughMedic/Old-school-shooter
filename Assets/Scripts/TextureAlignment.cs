using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class TextureAlignment : MonoBehaviour
{
    public bool update = false;

    void Start()
    {
        fixTextures();
    }

    private void Update()
    {
       if(update)
        {
            fixTextures();
        }
    }

    void fixTextures()
    {
        gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2(gameObject.transform.localScale.x / 5, gameObject.transform.localScale.y / 5);
        update = false;
    }
}
