using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    public bool isShadow;
    SpriteRenderer[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if (sprites == null)
        {
            sprites = GetComponentsInChildren<SpriteRenderer>();
        }
        if (!isShadow)
        {
            foreach (SpriteRenderer sprite in sprites)
                sprite.color = Color.white;
        }
        else
        {
            foreach (SpriteRenderer sprite in sprites)
                sprite.color = new Color32(0, 100, 170, 255);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
