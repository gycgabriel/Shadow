using UnityEngine;

/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

	new public string name = "New Item";	// Name of the item
	public Sprite icon = null;              // Item icon
	public bool isStackable;
	public int currentAmt = 1;
	[TextArea(3, 10)]
	public string description;
	[TextArea(3, 10)]
	public string flavorText;

	public int buyPrice;
	public int sellPrice;

	// Called when the item is pressed in the inventory
	public virtual void Use ()
	{
		// Use the item
		// Something might happen

		Debug.Log("Using " + name);
	}

	public void RemoveFromInventory ()
	{
		PartyController.inventory.Remove(this, true);
	}

	public void RemoveFromInventory(int amt)
	{
		PartyController.inventory.Remove(this, amt);
	}

	public int GetAmtInInventory()
    {
		return PartyController.inventory.GetItemAmt(this);

	}

	public void BoughtForGold(int amt)
    {
		PartyController.inventory.Gold -= buyPrice * amt;
		Item clone = Instantiate(this);
		clone.currentAmt = amt;
		PartyController.inventory.Add(clone);
	}

	public void SoldForGold(int amt)
    {
		PartyController.inventory.Gold += sellPrice * amt;
		PartyController.inventory.Remove(this, amt);
	}
}
