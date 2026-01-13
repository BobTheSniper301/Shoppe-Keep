using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour
{
    
    [SerializeField] GameObject healthBar;
    [SerializeField] Image healthBarCurrent;
    [SerializeField] Text goldText;

    public void UpdateStats()
    {
        Debug.Log("update stats");
        // Health bar
        healthBar.transform.localScale = new Vector3(1 * (1 + (PlayerScript.instance.maxHealth-100) * 0.0025f), 1, 1);
        healthBarCurrent.fillAmount = PlayerScript.instance.currentHealth / PlayerScript.instance.maxHealth;

        // Gold
        goldText.text = PlayerScript.instance.gold.ToString();
        // Move to vendor menu script
        // vendorMenuGoldAmount.GetComponent<Text>().text = PlayerScript.instance.gold.ToString();
    }




    void OnEnable()
    {
        EventManager.itemSale += UpdateStats;
    }

    void OnDisable()
    {
        EventManager.itemSale -= UpdateStats;
    } 
}
