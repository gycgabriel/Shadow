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
		itemOptionsWindow = FindObjectOfType<ItemOptions>(true);
    }

    public void OnPointerClick(PointerEventData eventData)
	{
		if (GetComponent<Button>().interactable == true && eventData.button == PointerEventData.InputButton.Right)
        {
			Debug.Log("Right click");
			GetComponentInParent<InventorySlot>().SelectItem();
			if (InventoryUI.selectedItem.GetType().IsSubclassOf(typeof(Consumable)))
            {
				itemOptionsWindow.gameObject.SetActive(true);
				itemOptionsWindow.transform.position = Input.mousePosition;
			}
			
		}
			
	}
}
