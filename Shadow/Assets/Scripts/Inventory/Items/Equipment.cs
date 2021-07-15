using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* An Item that can be equipped. */

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

	public EquipmentSlot equipSlot;	// Slot to store equipment in

	public int armorModifier;		// Increase/decrease in armor
	public int damageModifier;      // Increase/decrease in damage
    //public SkinnedMeshRenderer mesh;
    //public EquipmentManager.MeshBlendShape[] coveredMeshRegions;

	// When pressed in inventory
	public override void Use()
	{
		base.Use();
		//EquipmentManager.instance.Equip(this);	// Equip it
		EquipFromInventory();					// Remove it from inventory
	}

	public void EquipFromInventory()
	{
		PartyController.inventory.Remove(this, false);
	}

	public void UnequipIntoInventory()
	{
		PartyController.inventory.Add(this);
	}

}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }
