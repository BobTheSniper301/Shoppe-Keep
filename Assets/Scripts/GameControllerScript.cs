using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    [HideInInspector] public bool inMenu = false;
    public GameObject darkBackground;
    public GameObject overviewMenu;
    public PlayerScript playerScript;

    void Update()
    {
        if (!inMenu && Input.GetKey(KeyCode.Tab)) 
        {
            inMenu = true;
            overviewMenu.gameObject.SetActive(true);
        }
        if (inMenu)
        {
            darkBackground.gameObject.SetActive(true);
            playerScript.canMove = false;
        }
    }
}
