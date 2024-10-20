using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartridgeReceiver : Interactable
{
    public VictoryDoor victoryDoor;
    // Start is called before the first frame update
    void Start()
    {
        victoryDoor = GetComponentInParent<VictoryDoor>();
    }

    public override void OnActionInput(Inventory inventory)
    {
        // base.OnActionInput(inventory);
        AddCartridges();
    }

    public void AddCartridges()
    {
        foreach(KeyValuePair<string, int> item in inventory.inventory)
        {
            if(item.Value == 1)
                victoryDoor.InsertCartridge(item.Key);
        }

        if(victoryDoor.CheckCartridges())
        {
            pressActionObj.SetActive(false);
            inventory.currentInteractableObject = null;
            inventory = null;
            Collider collider = GetComponent<Collider>();
            collider.enabled = false;
        }
    }
}
