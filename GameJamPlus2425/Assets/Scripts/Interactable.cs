using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected Inventory inventory;
    public GameObject pressActionObj;

    public virtual void OnActionInput(Inventory inventory)
    {
        this.inventory = inventory;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            inventory = other.gameObject.GetComponent<Inventory>();
            inventory.currentInteractableObject = this;

            // Show UI
            pressActionObj.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            inventory.currentInteractableObject = null;
            inventory = null;

            // Hide UI
            pressActionObj.SetActive(false);
        }
    }
}
