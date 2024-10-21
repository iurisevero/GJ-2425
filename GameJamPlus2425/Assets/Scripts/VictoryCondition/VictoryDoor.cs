using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryDoor : MonoBehaviour
{
    private Collider winTrigger;
    private Dictionary<string, bool> insertedCartridges;
    public List<GameObject> cartridgesRedObjList;
    public List<GameObject> cartridgesBlueObjList;
    private Dictionary<string, GameObject> cartridgesRed;
    private Dictionary<string, GameObject> cartridgesBlue;
    public GameObject doorRed;
    public GameObject doorBlue;
    public GameObject altarRed;
    public GameObject altarBlue;

    // Start is called before the first frame update
    void Start()
    {
        winTrigger = GetComponent<Collider>();
        winTrigger.enabled = false;

        insertedCartridges = new Dictionary<string, bool>();
        cartridgesRed = new Dictionary<string, GameObject>();
        cartridgesBlue = new Dictionary<string, GameObject>();

        Cartridge[] cartridges = FindObjectsOfType<Cartridge>();
        int i = 0;
        foreach(Cartridge cartridge in cartridges)
        {
            Debug.Log("i:" + i);
            Debug.Log("List red count: " + cartridgesRedObjList.Count);
            Debug.Log("List blue count: " + cartridgesBlueObjList.Count);
            insertedCartridges[cartridge.cartridgeKey] = false;
            cartridgesRed[cartridge.cartridgeKey] = cartridgesRedObjList[i];
            cartridgesBlue[cartridge.cartridgeKey] = cartridgesBlueObjList[i];
            cartridgesRedObjList[i].SetActive(false);
            cartridgesBlueObjList[i].SetActive(false);
            i++;
        }

        doorRed.SetActive(true);
        doorBlue.SetActive(false);
        altarRed.SetActive(true);
        altarBlue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetWin()
    {
        StartCoroutine(WinCoroutine());
    }

    private IEnumerator WinCoroutine()
    {
        altarRed.SetActive(false);
        altarBlue.SetActive(true);

        yield return new WaitForSeconds(1f);

        foreach(string key in cartridgesRed.Keys){
            cartridgesRed[key].SetActive(false);
            cartridgesBlue[key].SetActive(true);
            yield return new WaitForSeconds(1f);
        }

        doorRed.SetActive(false);
        doorBlue.SetActive(true);
        AudioManager.Instance.Play("DoorOpen");
        winTrigger.enabled = true;
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
        cartridgesRed[cartridgeKey].SetActive(true);


        if(CheckCartridges())
        {
            SetWin();
        }
    }
}
