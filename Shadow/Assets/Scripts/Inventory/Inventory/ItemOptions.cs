using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemOptions : MonoBehaviour
{
	public Button selectedSlotBtn;

	public void OnBackButton()
    {
		gameObject.SetActive(false);
		selectedSlotBtn.Select();
		selectedSlotBtn.OnSelect(null);
	}
}
