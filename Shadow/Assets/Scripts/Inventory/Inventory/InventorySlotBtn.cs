using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotBtn : MonoBehaviour, ISelectHandler
{
    public ItemOptions itemOptionsWindow;

    private void Start()
    {
        itemOptionsWindow = GetComponentInParent<InventoryUI>().itemOptionsWindow;
        GetComponent<Button>().onClick.AddListener(() => 
        {
            itemOptionsWindow.gameObject.SetActive(true);
            itemOptionsWindow.transform.position = transform.position + new Vector3(30f, 0f);
            itemOptionsWindow.selectedSlotBtn = GetComponent<Button>();
        });
    }

    public void OnSelect(BaseEventData eventData)
    {
        GetComponentInParent<InventorySlot>().SelectItem();
    }
}
