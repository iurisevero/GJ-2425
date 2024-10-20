using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public  Dictionary<string, int> inventory;
    private InputHandler _input;
    public Interactable currentInteractableObject;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<InputHandler>();
        inventory = new Dictionary<string, int>();
        currentInteractableObject = null;
    }

    void Update()
    {
        if(_input.actionInput)
        {
            _input.actionInput = false;
            if(currentInteractableObject)
                currentInteractableObject.OnActionInput();
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
