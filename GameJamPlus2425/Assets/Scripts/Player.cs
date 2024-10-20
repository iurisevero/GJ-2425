using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private InputHandler _input;
    public  Dictionary<string, bool> inventory;
    public Interactable currentInteractableObject;
    public int maxHealth = 100;
    public int currentHealth;
    public PlayerUIController playerUIController;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        _input = GetComponent<InputHandler>();
        inventory = new Dictionary<string, bool>();

        Cartridge[] cartridges = FindObjectsOfType<Cartridge>();
        foreach(Cartridge cartridge in cartridges)
        {
            inventory[cartridge.cartridgeKey] = false;
        }

        currentInteractableObject = null;
        if(playerUIController != null){
            playerUIController.SetHealth(0, maxHealth);
            playerUIController.SetAmmo(0, 100);
            playerUIController.SetCartridge(inventory);
        }
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        playerUIController.UpdateHealth(currentHealth);
        AudioManager.Instance.Play("PlayerDamage");
        if (currentHealth == 0) {
            Die();
        }
    }

    public void Die()
    {
        // Play die animation
        playerUIController.ShowLoseScreen();
    }

    public void AddCartridge(string cartridge)
    {
        if(string.IsNullOrEmpty(cartridge)) return;

        inventory[cartridge] = true;
        playerUIController.UpdateCartridge(cartridge, true);
    }

    public void RemoveCartridge(string cartridge)
    {
        if(string.IsNullOrEmpty(cartridge)) return;

        inventory[cartridge] = false;
    }
}
