using UnityEngine;
using System.Threading.Tasks;

public class PotionCraftingStationScript : MonoBehaviour
{
    [SerializeField] PotionCraftAnimScript animScript;
    [SerializeField] GameObject cameraSpotObj;
    Vector3 originalCamPos;

    public void OpenCrafting()
    {
        UiManager.instance.canOpenMenu = false;
        PlayerScript.instance.canMove = false;
        UiManager.instance.inMenu = true;
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("opencrafting");
        UiManager.instance.craftingMenu.SetActive(true);
    }


    public void QuitCrafting()
    {
        UiManager.instance.canOpenMenu = true;
        PlayerScript.instance.canMove = true;
        UiManager.instance.inMenu = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("quitcrafting");
        UiManager.instance.craftingMenu.SetActive(false);
        if (originalCamPos != new Vector3(0, 0, 0))
        {
            PlayerScript.instance.playerCamera.transform.position = originalCamPos;
        }
        originalCamPos = new Vector3(0, 0, 0);
    }


    public async Task Craft()
    {
        originalCamPos = PlayerScript.instance.playerCamera.transform.position;
        while (Vector3.Distance(PlayerScript.instance.playerCamera.transform.position, cameraSpotObj.transform.position) >= 0.01f)
        {
            PlayerScript.instance.playerCamera.transform.position = Vector3.MoveTowards(PlayerScript.instance.playerCamera.transform.position, cameraSpotObj.transform.position, 0.1f);
            await Task.Delay(2);
        }

        PlayerScript.instance.playerCamera.transform.rotation = cameraSpotObj.transform.rotation;

        Debug.Log("craft");
        await animScript.CraftAnim();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            QuitCrafting();
            UiManager.instance.MenuOpen(null);
        }
    }
}
