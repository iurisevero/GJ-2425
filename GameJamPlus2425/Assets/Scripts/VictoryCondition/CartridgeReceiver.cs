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

    public override void OnActionInput()
    {
        AddCartridges();
    }

    public void AddCartridges()
    {
        foreach(KeyValuePair<string, bool> item in player.inventory)
        {
            if(item.Value)
                victoryDoor.InsertCartridge(item.Key);
        }

        if(victoryDoor.CheckCartridges())
        {
            player.playerUIController.HidePressAction();
            player.currentInteractableObject = null;
            player = null;
            Collider collider = GetComponent<Collider>();
            collider.enabled = false;
        }
    }
}
