using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickItemOption : MonoBehaviour, IPointerClickHandler
{
	public ItemOptions itemOptionsWindow;

    private void Start()
    {
		itemOptionsWindow = GetComponentInParent<InventoryUI>().itemOptionsWindow;
    }

    public void OnPointerClick(PointerEventData eventData)
	{
		if (GetComponent<Button>().interactable == true && eventData.button == PointerEventData.InputButton.Right)
        {
			Debug.Log("Right click");
			GetComponentInParent<InventorySlot>().SelectItem();
			itemOptionsWindow.gameObject.SetActive(true);
			itemOptionsWindow.transform.position = Input.mousePosition;
			
		}
			
	}
}
