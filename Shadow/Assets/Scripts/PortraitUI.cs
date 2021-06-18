using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PortraitUI : MonoBehaviour
{
    public bool isStatus;
    public bool isShadow;
    public PortraitBehaviour portraitOf;
    private Image image;

    void Start()
    {
        if (isShadow)
        {
            portraitOf = PartyController.shadow.GetComponent<PortraitBehaviour>();
        } 
        else
        {
            portraitOf = PartyController.player.GetComponent<PortraitBehaviour>();
        }
        image = GetComponent<Image>();
        image.sprite = portraitOf.portraitToDisplay;
    }

    private void Update()
    {
        if (isStatus)
        {
            portraitOf = PartyController.activePC.gameObject.GetComponent<PortraitBehaviour>();
        }
        image.sprite = portraitOf.portraitToDisplay;
    }

}
