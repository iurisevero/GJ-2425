using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cartridge : Interactable
{
    // UI de ação temporário
    public string cartridgeKey = "";

    public override void OnActionInput(Inventory inventory)
    {
        // base.OnActionInput(inventory);
        inventory.AddCartridge(cartridgeKey);
        DestroyCartridge();
    }

    public void DestroyCartridge()
    {
        pressActionObj.SetActive(false);
        inventory.currentInteractableObject = null;
        inventory = null;
        Destroy(this.gameObject);
    } 
}
