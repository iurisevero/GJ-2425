using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
        
    // UI de ação temporário
    public GameObject pressActionObj;
    public GameObject loseScreen;
    public GameObject winScreen;
    public Slider healthBar;
    public Slider ammoSlider;
    public TextMeshProUGUI currentAmmoText;
    public List<Image> cartridgesImg;
    public Color32 cartridgeEnableColor;
    public Color32 cartridgeDisableColor;
    Dictionary<string, Image> cartridgesImgMap;


    // Start is called before the first frame update
    void Start()
    {
        cartridgesImgMap = new Dictionary<string, Image>();
    }


    public void SetHealth(int minHealth, int maxHealth)
    {
        healthBar.minValue = minHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }
    public void UpdateHealth(int currentHealth)
    {
        Debug.Log(currentHealth);
        healthBar.value = currentHealth;
    }

    public void SetAmmo(int minAmmo, int maxAmmo)
    {
        ammoSlider.minValue = minAmmo;
        ammoSlider.maxValue = maxAmmo;
        ammoSlider.value = maxAmmo;
        currentAmmoText.text = maxAmmo.ToString();
    }
    public void UpdateAmmo(int currentAmmo)
    {
        currentAmmoText.text = currentAmmo.ToString();
        ammoSlider.value = currentAmmo;
    }
    public void ReloadAmmo(float reloadTime)
    {
        StartCoroutine(ReloadAmmoRoutine(reloadTime - 0.1f));
    }
    private IEnumerator ReloadAmmoRoutine(float reloadTime)
    {
        float minValue = ammoSlider.minValue;
        float maxValue = ammoSlider.maxValue;

        ammoSlider.minValue = 0;
        ammoSlider.maxValue = 1;
        float currentTime = 0;
        while(currentTime < reloadTime)
        {
            currentTime += Time.deltaTime;
            ammoSlider.value = currentTime / reloadTime;
            yield return new WaitForEndOfFrame();
        }
        ammoSlider.minValue = minValue;
        ammoSlider.maxValue = maxValue;
    }

    public void SetCartridge(Dictionary<string, bool> inventory)
    {
        // int i = 0;
        // Debug.Log("SetCartridge: "  + inventory);
        // Debug.Log("cartridgesImg: " + cartridgesImg.Count);
        // foreach(KeyValuePair<string, bool> cartridge in inventory) {
        //     Debug.Log("cartridge: " + cartridge.Key);
        //     cartridgesImgMap[cartridge.Key] = cartridgesImg[i];
        //     cartridgesImg[i].color = cartridge.Value ? cartridgeDisableColor : cartridgeEnableColor;
        //     i++;
        // }
    }

    public void UpdateCartridge(string cartridge, bool found)
    {
        // if(found)
        //     cartridgesImgMap[cartridge].color = cartridgeDisableColor;
        // else
        //     cartridgesImgMap[cartridge].color = cartridgeEnableColor;
    }

    public void ShowPressAction()
    {
        pressActionObj.SetActive(true);
    }

    public void HidePressAction()
    {
        pressActionObj.SetActive(false);
    }

    public void ShowLoseScreen()
    {

    }

    public void ShowWinScreen()
    {

    }
}
