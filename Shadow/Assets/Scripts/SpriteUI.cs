using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteUI : MonoBehaviour
{
    public bool isShadow;
    private Image image;
    private PortraitBehaviour spriteOf;

    void Start()
    {
        if (isShadow)
        {
            spriteOf = PartyController.shadow.GetComponent<PortraitBehaviour>();
        }
        else
        {
            spriteOf = PartyController.player.GetComponent<PortraitBehaviour>();
        }
        image = GetComponent<Image>();
        image.sprite = spriteOf.spriteToDisplay;
    }

}
