using UnityEngine;
using UnityEngine.UI;

public class OverviewMenuScript : MonoBehaviour
{
    [SerializeField] MenuManager menuManager;
    [SerializeField] GameObject menu;
    // Stats
    [SerializeField] Text maxHealthNum;

    void UpdateStats()
    {
        maxHealthNum.text = PlayerScript.instance.maxHealth.ToString();

    }

    void OnEnable()
    {
        EventManager.itemSale += UpdateStats;
    }

    void OnDisable()
    {
        EventManager.itemSale -= UpdateStats;
    } 



    void Update()
    {
        if (Input.GetKeyDown("tab"))
        {
            if (menuManager.activeMenu == menu)
            {
                menu.SetActive(false);
                menuManager.activeMenu = null;
                return;
            }
            else if (menuManager.activeMenu != null)
            {
                menuManager.activeMenu.SetActive(false);
            }
            menu.SetActive(true);
            menuManager.activeMenu = menu;
        }
        
    }
}
