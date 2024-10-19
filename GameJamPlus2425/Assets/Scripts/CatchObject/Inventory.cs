using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<string, int> inventory;
    private InputHandler _input;
    // Trocar lógica de pegar objeto triggado do inventário para o player no futuro 
    // Isso inclui o input handler e o update
    // E o postNotification
    public static string AllCartridgeFoundEvent = "Inventory.AllCartridgeFoundEvent";
    public int totalCartridge = 5;
    public Cartridge currentTriggeredCartridge;

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
            AddCartridge(currentTriggeredCartridge);
        }
    }

    public void AddCartridge(Cartridge cartridge)
    {
        if(cartridge == null) return;

        inventory[cartridge.cartridgeKey] = 1;
        cartridge.DestroyCartridge();
    }

    public void RemoveCartridge(Cartridge cartridge)
    {
        if(cartridge == null) return;

        inventory[cartridge.cartridgeKey] = 0;
    }
}
