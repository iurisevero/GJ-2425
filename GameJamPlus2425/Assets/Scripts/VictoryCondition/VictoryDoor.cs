using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryDoor : MonoBehaviour
{
    private Collider winTrigger;
    private Dictionary<string, bool> insertedCartridges;

    // Start is called before the first frame update
    void Start()
    {
        winTrigger = GetComponent<Collider>();
        winTrigger.enabled = false;

        insertedCartridges = new Dictionary<string, bool>();

        Cartridge[] cartridges = FindObjectsOfType<Cartridge>();
        foreach(Cartridge cartridge in cartridges)
        {
            insertedCartridges[cartridge.cartridgeKey] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetWin()
    {
        winTrigger.enabled = true;
        // Do some animation or cinematic
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") {
            Debug.Log("Victory");
            Player player = other.GetComponent<Player>();
            player.playerUIController.ShowWinScreen();
        }
    }

    public bool CheckCartridges()
    {
        bool canWin = true;
        foreach(bool item in insertedCartridges.Values)
            canWin = canWin && item;
        
        return canWin;
    }

    public void InsertCartridge(string cartridgeKey)
    {
        insertedCartridges[cartridgeKey] = true;

        if(CheckCartridges())
        {
            SetWin();
        }
    }
}
