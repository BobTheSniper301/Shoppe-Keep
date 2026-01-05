// using UnityEngine;

// public class PlayerLookScript : MonoBehaviour
// {

//     private Ray cameraRay;

//     LayerMask playerLookMask;
//     LayerMask playerLookPriceMask;
//     LayerMask playerLookVendorMask;
//     LayerMask playerLookCraftingMask;


//     [SerializeField] float PlayerLookDistance;


//     [SerializeField] new Camera camera;

//     // TODO: Need to fix up this trash check where looking stuff (could use on trigger stay from another object)
//     void VendorCheck()
//     {
//         if (Physics.Raycast(cameraRay, out PlayerScript.instance.vendorHit, PlayerLookDistance + 2, playerLookVendorMask) && ! UiManager.instance.inMenu)
//         {
//             UiManager.instance.vendorPrompt.SetActive(true);
//             if (Input.GetKeyDown("f"))
//             {
//                 UiManager.instance.VendorMenu();
//             }
//         }
//         else
//         {
//             UiManager.instance.vendorPrompt.SetActive(false);
//         }
//     }


//     void CraftingPromptCheck()
//     {
//         if (Physics.Raycast(cameraRay, out PlayerScript.instance.craftingHit, PlayerLookDistance + 2, playerLookCraftingMask) && !UiManager.instance.inMenu)
//         {
//             UiManager.instance.craftingPrompt.SetActive(true);
//             if (Input.GetKeyDown("f"))
//             {
//                 Debug.Log("f");
//                 PlayerScript.instance.currentCraftingStation = PlayerScript.instance.craftingHit.transform.parent.gameObject;
//                 UiManager.instance.craftingMenu.GetComponent<CraftingMenuScript>().OpenCrafting();
//             }
//         }
//         else
//         {
//             UiManager.instance.craftingPrompt.SetActive(false);
//         }
//     }


//     // Checks if looking at an item container
//     void ItemContainerCheck()
//     {

//         if (Physics.Raycast(cameraRay, out PlayerScript.instance.containerHit, PlayerLookDistance, playerLookMask) && !UiManager.instance.inMenu && !UiManager.instance.canChangePrice)
//         {
//             UiManager.instance.ItemContainerPrompt(true);
//         }
//         else
//         {
//             UiManager.instance.ItemContainerPrompt(false);
//         }

//     }


//    // Checks if looking at a price change interface
//     void ItemPriceChangeLook()
//     {
//         if (Physics.Raycast(cameraRay, out PlayerScript.instance.priceChangeHit, PlayerLookDistance, playerLookPriceMask))
//         {
//             PedestalScript pedestal = PlayerScript.instance.priceChangeHit.transform.parent.root.GetComponent<PedestalScript>();

//             UiManager.instance.canChangePrice = true;
//             // Debug.Log(PlayerScript.instance.PlayerScript.instance.priceChangeHit.transform.name);
//             if (Input.GetKeyDown(KeyCode.Mouse0))
//             {
//                 // Debug.Log("Mouse down!");
//                 pedestal.PedestalChange(PlayerScript.instance.priceChangeHit.transform.gameObject.name, PlayerScript.instance.priceChangePower);
//                 // TODO: Remove later when purchase does not happen on pedestal from player
//                 // Incase a purchase happens
//                 PlayerScript.playerStatChanged?.Invoke();
//             }

//         }
//         else
//         {
//             UiManager.instance.canChangePrice = false;
//         }

//     }

//     private void Awake()
//     {
//         playerLookMask = LayerMask.GetMask("PlayerLookContainer");

//         playerLookPriceMask = LayerMask.GetMask("PlayerLookPrice");

//         playerLookVendorMask = LayerMask.GetMask("Vendor");

//         playerLookCraftingMask = LayerMask.GetMask("PlayerLookCrafting");
//     }


//     void Start()
//     {
//         Cursor.lockState = CursorLockMode.Locked;
//     }


//     void Update()
//     {
//         // Ray shoots out from center of player view
//         cameraRay = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

//         ItemContainerCheck();

//         ItemPriceChangeLook();

//         VendorCheck();

//         CraftingPromptCheck();

//         // Debug.DrawRay(cameraRay.origin, cameraRay.direction * PlayerLookDistance, Color.red, 0.5f);
//     }
// }
