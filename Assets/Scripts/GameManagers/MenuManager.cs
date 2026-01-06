using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private GameObject[] allMenus;

    public GameObject activeMenu;

    [SerializeField] GameObject darkBackground;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        allMenus = GameObject.FindGameObjectsWithTag("Menu");
        foreach (GameObject menu in allMenus)
        {
            menu.SetActive(false);
        }

        darkBackground.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
