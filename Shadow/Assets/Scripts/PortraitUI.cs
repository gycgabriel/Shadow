using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PortraitUI : MonoBehaviour
{
    public PortraitBehaviour portraitOf;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = portraitOf.portraitToDisplay;
    }

}
