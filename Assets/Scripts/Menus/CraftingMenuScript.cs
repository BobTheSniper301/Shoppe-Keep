// using UnityEngine;
// using System.Threading.Tasks;

// public class CraftingMenuScript : MonoBehaviour
// {
//     public ItemScript[] inputItems;
//     public GameObject outputItem;
//     public GameObject outputItemInst;
//     public GameObject[] recipes;

//     public bool craftableRecipe;

//     [SerializeField] MonoBehaviour animScript;
//     [SerializeField] GameObject cameraSpotObj;
//     Vector3 originalCamPos;


//     public async void StartCraft()
//     {
//         Debug.Log("craft");

//         animScript = PlayerScript.instance.currentCraftingStation.GetComponent<MonoBehaviour>();
//         cameraSpotObj = PlayerScript.instance.currentCraftingStation.transform.Find("CameraSpot").gameObject;

//         int i = 0;
//         foreach (ItemScript item in this.GetComponentsInChildren<ItemScript>())
//         {
//             inputItems[i] = item;
//             i++;
//         }
//         if (inputItems[0] != null && inputItems[1] != null)
//         {
//             CheckRecipes();
//         }
//         if (craftableRecipe == false)
//         {
//             return;
//         }

//         originalCamPos = PlayerScript.instance.playerCamera.transform.position;
//         while (Vector3.Distance(PlayerScript.instance.playerCamera.transform.position, cameraSpotObj.transform.position) >= 0.01f)
//         {
//             PlayerScript.instance.playerCamera.transform.position = Vector3.MoveTowards(PlayerScript.instance.playerCamera.transform.position, cameraSpotObj.transform.position, 0.1f);
//             await Task.Delay(2);
//         }
//         PlayerScript.instance.playerCamera.transform.rotation = cameraSpotObj.transform.rotation;

//         animScript.Invoke("CraftAnim", 0);
//     }

//     void CheckRecipes()
//     {
//         if (inputItems[0].gameObject.name == "Pickaxe" && inputItems[1].gameObject.name == "Shovel")
//         {
//             Debug.Log("recipe found: 1");
//             outputItem = recipes[0];
//             craftableRecipe = true;
//             ClearInput();
//         }
//         else if (inputItems[0].gameObject.name == "Shovel" && inputItems[1].gameObject.name == "Pickaxe")
//         {
//             Debug.Log("recipe found: 2");
//             outputItem = recipes[1];
//             craftableRecipe = true;
//             ClearInput();
//         }
//         else
//         {
//             Debug.Log("recipe not found");
//             craftableRecipe = false;
//         }
//     }


//     public void CraftItem()
//     {
//         outputItemInst = Instantiate(outputItem);
//         UiManager.instance.SpawnGroundItem(outputItemInst, new Vector3(PlayerScript.instance.transform.position.x, 0, PlayerScript.instance.transform.position.z));

//         QuitCrafting();
//     }


//     void ClearInput()
//     {
//         foreach (ItemScript item in inputItems)
//         {
//             Destroy(item.gameObject);
//         }
//     }



//     public void OpenCrafting()
//     {
//         UiManager.instance.canOpenMenu = false;
//         PlayerScript.instance.canMove = false;
//         UiManager.instance.inMenu = true;
//         Cursor.lockState = CursorLockMode.None;
//         UiManager.instance.craftingMenu.SetActive(true);
//     }


//     public void QuitCrafting()
//     {
//         UiManager.instance.canOpenMenu = true;
//         PlayerScript.instance.canMove = true;
//         UiManager.instance.inMenu = false;
//         Cursor.lockState = CursorLockMode.Locked;
//         UiManager.instance.craftingMenu.SetActive(false);
//         if (originalCamPos != new Vector3(0, 0, 0))
//         {
//             PlayerScript.instance.playerCamera.transform.position = originalCamPos;
//         }
//         originalCamPos = new Vector3(0, 0, 0);
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown("escape"))
//         {
//             QuitCrafting();
//             UiManager.instance.MenuOpen(null);
//         }
//     }
// }
