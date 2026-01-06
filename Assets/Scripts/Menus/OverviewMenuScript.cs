using UnityEngine;

public class OverviewMenuScript : MonoBehaviour
{
    [SerializeField] MenuManager menuManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("tab"))
        {
            this.gameObject.SetActive(true);
        }
        //if (menu)
    }
}
