using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected Player player;
    
    // UI de ação temporário
    public GameObject pressActionObj;

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
            pressActionObj.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            player.currentInteractableObject = null;
            player = null;

            // Hide UI
            pressActionObj.SetActive(false);
        }
    }
}
