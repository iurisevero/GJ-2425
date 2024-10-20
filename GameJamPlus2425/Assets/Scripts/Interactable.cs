using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected Player player;

    public virtual void OnActionInput()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player = other.gameObject.GetComponent<Player>();
            player.currentInteractableObject = this;

            // Show UI
            player.playerUIController.ShowPressAction();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            // Hide UI
            player.playerUIController.HidePressAction();
            
            player.currentInteractableObject = null;
            player = null;
        }
    }
}
