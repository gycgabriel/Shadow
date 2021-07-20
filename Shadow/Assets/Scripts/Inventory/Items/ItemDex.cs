using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDex : MonoBehaviour
{
    [SerializeField]
    private List<Item> items;

    public Item GetItem(int itemNumber)
    {
        return items[itemNumber - 1];
    }
}
