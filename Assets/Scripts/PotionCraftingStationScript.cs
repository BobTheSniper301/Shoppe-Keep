using UnityEngine;
using System.Threading.Tasks;

public class PotionCraftingStationScript : MonoBehaviour
{
    [SerializeField] PotionCraftAnimScript animScript;
    [SerializeField] GameObject cameraSpotObj;
    Vector3 cameraSpot;
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
        PlayerScript.instance.playerCamera.transform.position = originalCamPos;
    }


    public async Task Craft()
    {
        cameraSpot = cameraSpotObj.transform.position;
        originalCamPos = playerCamPos;
        while (Vector3.Distance(PlayerScript.instance.playerCamera.transform.position, cameraSpot) >= 0.11f)
        {
            Debug.Log("move Camera");
            PlayerScript.instance.playerCamera.transform.position = Vector3.MoveTowards(PlayerScript.instance.playerCamera.transform.position, cameraSpot, 0.1f);
            PlayerScript.instance.playerCamera.transform.rotation = Quaternion.RotateTowards(PlayerScript.instance.playerCamera.transform.rotation, cameraSpotObj.transform.rotation, 1);
            await Task.Delay(2);
        }
        
        Debug.Log("craft");
        animScript.CraftAnim();
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
