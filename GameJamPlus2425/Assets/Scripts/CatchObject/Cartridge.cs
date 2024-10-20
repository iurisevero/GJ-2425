using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cartridge : Interactable
{
    public string cartridgeKey = "";

    public override void OnActionInput()
    {
        // base.OnActionInput(inventory);
        player.AddCartridge(cartridgeKey);
        DestroyCartridge();
    }

    public void DestroyCartridge()
    {
        pressActionObj.SetActive(false);
        player.currentInteractableObject = null;
        player = null;
        Destroy(this.gameObject);
    } 
}
