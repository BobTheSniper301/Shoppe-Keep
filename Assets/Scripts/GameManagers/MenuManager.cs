using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public GameObject activeMenu;

    [SerializeField] GameObject darkBackground;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] allMenus = GameObject.FindGameObjectsWithTag("Menu");
        foreach (GameObject menu in allMenus)
        {
            menu.SetActive(false);
        }

        darkBackground.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (activeMenu != null)
        {
            // player cannot move
            darkBackground.SetActive(true);
        }
        else
        {
            darkBackground.SetActive(false);
        }
    }
}
