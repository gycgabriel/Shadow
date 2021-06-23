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

        GetComponent<Image>().sprite = portraitOf.portraitToDisplay;
    }

    private void Update()
    {
        if (isStatus)
        {
            portraitOf = PartyController.activePC.gameObject.GetComponent<PortraitBehaviour>();
        }

        if (portraitOf.portraitToDisplay != null)
        {
            GetComponent<Image>().sprite = portraitOf.portraitToDisplay;
        }
    }

}
