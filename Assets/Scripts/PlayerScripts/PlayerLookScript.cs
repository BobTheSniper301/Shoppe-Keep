using UnityEngine;

public class PlayerLookScript : MonoBehaviour
{

    private Ray cameraRay;

    LayerMask playerLookMask;
    LayerMask playerLookPriceMask;


    [SerializeField] float PlayerLookDistance;


    [SerializeField] new Camera camera;

    // Checks if looking at an item container
    void ItemContainerCheck()
    {

        if (Physics.Raycast(cameraRay, out PlayerScript.instance.containerHit, PlayerLookDistance, playerLookMask) && !UiManager.instance.inMenu && !UiManager.instance.canChangePrice)
        {

            UiManager.instance.ContainerText(true);

        }
        else
        {

            UiManager.instance.ContainerText(false);

        }

    }

    void ItemPriceChangeLook()
    {
        if (Physics.Raycast(cameraRay, out PlayerScript.instance.priceChangeHit, PlayerLookDistance, playerLookPriceMask))
        {

            PedestalScript pedestal = PlayerScript.instance.priceChangeHit.transform.parent.root.GetComponent<PedestalScript>();

            UiManager.instance.canChangePrice = true;
            // Debug.Log(PlayerScript.instance.PlayerScript.instance.priceChangeHit.transform.name);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                // Debug.Log("Mouse down!");
                pedestal.PedestalChange(PlayerScript.instance.priceChangeHit.transform.gameObject.name, PlayerScript.instance.priceChangePower);
                // TODO: Remove later when purchase does not happen on pedestal from player
                // Incase a purchase happens
                PlayerScript.playerStatChanged?.Invoke();
            }

        }
        else
        {

            UiManager.instance.canChangePrice = false;
        }

        // Debug.Log("canchangeprice: " + UiManager.instance.canChangePrice);

    }

    private void Awake()
    {
        playerLookMask = LayerMask.GetMask("PlayerLookContainer");

        playerLookPriceMask = LayerMask.GetMask("PlayerLookPrice");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraRay = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        ItemContainerCheck();

        ItemPriceChangeLook();


        // Debug.DrawRay(cameraRay.origin, cameraRay.direction * PlayerLookDistance, Color.red, 0.5f);
    }
}
