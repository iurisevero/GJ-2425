using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public  Dictionary<string, int> inventory;
    private InputHandler _input;
    // Trocar lógica de pegar objeto triggado do inventário para o player no futuro 
    // Isso inclui o input handler e o update
    // E o postNotification
    public static string AllCartridgeFoundEvent = "Inventory.AllCartridgeFoundEvent";
    public int totalCartridge = 5;
    public Cartridge currentTriggeredCartridge;
    public CartridgeReceiver currentCartridgeReceiver;
    public Interactable currentInteractableObject;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<InputHandler>();
        inventory = new Dictionary<string, int>();
        currentTriggeredCartridge = null;
    }

    void Update()
    {
        if(_input.actionInput)
        {
            _input.actionInput = false;
            if(currentInteractableObject)
                currentInteractableObject.OnActionInput(this);
        }
    }

    public void AddCartridge(string cartridge)
    {
        if(string.IsNullOrEmpty(cartridge)) return;

        inventory[cartridge] = 1;
    }

    public void RemoveCartridge(string cartridge)
    {
        if(string.IsNullOrEmpty(cartridge)) return;

        inventory[cartridge] = 0;
    }
}
