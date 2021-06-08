using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PortraitUI : MonoBehaviour
{
    public string who;              // player or shadow
    public PortraitBehaviour portraitOf;
    private Image image;

    void Start()
    {
        if (who == "player")
        {
            portraitOf = FindObjectOfType<Player>().gameObject.GetComponent<PortraitBehaviour>();
        }
        image = GetComponent<Image>();
        image.sprite = portraitOf.portraitToDisplay;
    }

}
