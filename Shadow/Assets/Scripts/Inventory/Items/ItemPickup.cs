using UnityEngine;

public class ItemPickup : Interactable {

	public Item item;   // Item to put in the inventory on pickup
	public int itemAmt;

	// When the player interacts with the item
	public override void Interact()
	{
		PickUp();	// Pick it up!
	}

	// Pick up the item
	void PickUp ()
	{
		Debug.Log("Picking up " + item.name);
		bool wasPickedUp;
		if (itemAmt > 1)
		{
			Item clone = Instantiate(item);
			clone.currentAmt = itemAmt;
			wasPickedUp = PartyController.inventory.Add(clone);
		}
		else
		{
			wasPickedUp = PartyController.inventory.Add(item);  // Add to inventory
		}

		// If successfully picked up
		if (wasPickedUp)
			Destroy(gameObject);	// Destroy item from scene
	}

}
