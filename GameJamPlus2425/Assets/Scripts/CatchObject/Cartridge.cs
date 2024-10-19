using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cartridge : MonoBehaviour
{
    // Start is called before the first frame update
    private Inventory inventory;
    public string cartridgeKey = "";
    // UI de ação temporário
    public GameObject pressActionObj;

    public void DestroyCartridge()
    {
        pressActionObj.SetActive(false);
        inventory.currentTriggeredCartridge = null;
        inventory = null;
        Destroy(this.gameObject);
    } 

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            inventory = other.gameObject.GetComponent<Inventory>();
            inventory.currentTriggeredCartridge = this;

            // Show UI
            pressActionObj.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            inventory.currentTriggeredCartridge = null;
            inventory = null;

            // Hide UI
            pressActionObj.SetActive(false);
        }
    }
}
