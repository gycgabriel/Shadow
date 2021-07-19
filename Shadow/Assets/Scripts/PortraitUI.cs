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

    void Update()
    {
        if (PartyController.player == null)
            return;

        if (isShadow)
        {
            portraitOf = PartyController.shadow.GetComponent<PortraitBehaviour>();
        }
        else
        {
            portraitOf = PartyController.player.GetComponent<PortraitBehaviour>();
        }

        GetComponent<Image>().sprite = portraitOf.portraitToDisplay;

        if (isStatus)
        {
            portraitOf = PartyController.activePC.gameObject.GetComponent<PortraitBehaviour>();

            // Set normal color theme if Player, darker color theme if Shadow
            if (!PartyController.shadowActive)
            {
                GetComponent<Image>().color = Color.white;
            }
            else
            {
                GetComponent<Image>().color = new Color32(0, 100, 170, 255);
            }
        }

        if (portraitOf.portraitToDisplay != null)
        {
            GetComponent<Image>().sprite = portraitOf.portraitToDisplay;
        }
    }

}
