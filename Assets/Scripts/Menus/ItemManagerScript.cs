using UnityEngine;

public class ItemManagerScript : MonoBehaviour
{
    //     // Vars for items
    //     // Set in inspector first
    //     public ItemData[] itemsData;

    //     public ItemScript[] items;

    //     public ItemSlotScript[] itemSlots;

    //     [HideInInspector] public GameObject itemPickedUp;

    //     public GameObject selectedItem;

    //     [HideInInspector] public int lastItemSlotNum;

    //     public bool isInventoryFull;

    //     #endregion  


    //     #region Items
    //     // Gets a list of all items
    //     public void GetItems()
    //     {
    //         Debug.Log("get items");

    //         clearItems();

    //         int i = 0;
    //         // Check each of the items slots to see if they have an item in them
    //         foreach (ItemSlotScript slot in itemSlots)
    //         {

    //             // Ensures itemslots show correct text for (non)stackable items
    //             slot.UpdateItemSlot();

    //             // If the itemslot does have an item, it stores it into the items list at the proper index
    //             if (slot.GetComponentInChildren<ItemScript>())
    //             {

    //                 items[i] = slot.GetComponentInChildren<ItemScript>();
    //                 itemsData[i] = items[i].itemData;

    //             }

    //             i++;
    //         }
    //         for (int j = 0; j < items.Length; j++)
    //         {
    //             if (items[j] == null)
    //             {
    //                 isInventoryFull = false;
    //             }
    //             else if (j == items.Length)
    //             {
    //                 isInventoryFull = true;
    //             }
    //         }
    //     }


    //     // Gets a list of all item slots
    //     void GetItemSlots()
    //     {

    //         itemSlots = GetComponentsInChildren<ItemSlotScript>();

    //     }


    //     // Clears the item list before updating it
    //     public void clearItems()
    //     {

    //         for (int i = 0; i < items.Length; i++)
    //         {

    //             items[i] = null;
    //             itemsData[i] = null;
    //         }
    //     }


    //     // To deselect all itemslots
    //     void DeselectAll()
    //     {

    //         foreach (ItemSlotScript i in itemSlots)
    //         {
    //             i.Deselected();
    //         }
    //         selectedItem = null;
    //     }


    //     // Selects a hotbar slot depending upon input
    //     void SelectHotbar()
    //     {

    //         for (int i = 0; i < items.Length; i++)
    //             if (Input.GetKeyDown((i + 1).ToString()))
    //                 {
    //                     // For select/deselect same slot
    //                     itemSlots[i].ToggleSelect();

    //                     DeselectAll();

    //                     if (!itemSlots[i].toggled)
    //                     {

    //                         itemSlots[i].Selected();

    //                     }

    //                 }

    //     }


    //     // Clears all current item related data - items[], itemsData[], etc - and destroys all items in slots
    //     public void ClearInventory()
    //     {
    //         int i = 0;
    //         while (i < items.Length)
    //         {

    //             items[i] = null;
    //             itemsData[i] = null;
    //             itemSlots[i].GetComponentInChildren<Text>().text = "";
    //             if (itemSlots[i].GetComponentInChildren<ItemScript>() != null)
    //             {

    //                 Destroy(itemSlots[i].GetComponentInChildren<ItemScript>().gameObject);

    //             }
    //             i++;

    //         }
    //     }


    //     // Adds a nonStackable item to the inventory in the first open slot (Adjusts transform values)
    //     public void AddItem(GameObject itemToAdd)
    //     {
    //         foreach (ItemSlotScript i in itemSlots)
    //         {
    //             if (i.GetComponentInChildren<ItemScript>() == null)
    //             {
    //                 itemToAdd.transform.SetParent(i.transform);
    //                 itemToAdd.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    //                 itemToAdd.transform.localRotation = new Quaternion(0, 0, 0, 0);
    //                 itemToAdd.GetComponent<ItemScript>().itemData = Instantiate(itemToAdd.GetComponent<ItemScript>().itemData);

    //                 break;
    //             }
    //         }
    //     }


    //     // Adds a Stackable item to the inventory, will add to the count of the same stackable item if it's in the inventory
    //     // If a stackable item of the same name is not in, it will treat it as a normal item and add it into the first open slot
    //     public void AddStackableItem(GameObject itemToAdd)
    //     {
    //         bool didStack = false;
    //         foreach (ItemScript i in items)
    //         {

    //             if (i is ItemScript && itemToAdd.GetComponent<ItemScript>().itemData.itemName == i.itemData.itemName)
    //             {

    //                 Destroy(itemToAdd);
    //                 i.itemData.count += itemToAdd.GetComponent<ItemScript>().itemData.count;
    //                 didStack = true;
    //                 break;
    //             }
    //         }
    //         if (didStack == false)
    //         {
    //             AddItem(itemToAdd);
    //         }
    //     }


    //     // Called when an item is picked up; will do AddStackableItem() or AddItem() depending on type
    //     public void PickUpItem(GameObject itemToAdd)
    //     {
    //         if (itemToAdd.GetComponent<ItemScript>().itemData.itemType == ItemData.ItemType.STACKABLE)
    //         {

    //             AddStackableItem(itemToAdd);

    //         }
    //         else
    //         {

    //             AddItem(itemToAdd);

    //         }
    //         GetItems();
    //     }


    //     public void SpawnGroundItem(GameObject item, Vector3 spawnLocation)
    //     {
    //         Instantiate(item);
    //         item.transform.localScale = new Vector3(10,10,10);
    //         item.transform.position = spawnLocation;
    //     }


    //     // Drops the selected item, goes forward from camera rotation, and adjusts transform values
    //     public void DropItem()
    //     {
    //         if (selectedItem)
    //         {

    //             selectedItem.transform.rotation = camera.transform.rotation;
    //             selectedItem.transform.eulerAngles = new Vector3(0, selectedItem.transform.rotation.eulerAngles.y, selectedItem.transform.rotation.eulerAngles.z);

    //             selectedItem.transform.SetParent(null);

    //             selectedItem.transform.position = PlayerScript.instance.gameObject.transform.position;
    //             selectedItem.transform.Translate(transform.forward * 5);

    //             selectedItem.transform.localScale = new Vector3(10, 10, 10);

    //             selectedItem.GetComponent<SphereCollider>().enabled = true;
    //         }
    //         DeselectAll();
    //         GetItems();
    //     }


    //     // Takes the selected item, moves it to the container, and adjusts transform values
    //     public void AddItemToContainer()
    //     {
    //         if (selectedItem != null)
    //         {
    //             selectedItem.transform.SetParent(PlayerScript.instance.containerHit.transform.parent);
    //             selectedItem.transform.position = PlayerScript.instance.containerHit.transform.parent.position;
    //             selectedItem.transform.rotation = PlayerScript.instance.containerHit.transform.parent.rotation;
    //             selectedItem.transform.localScale = new Vector3(5, 5, 5);
    //             selectedItem.GetComponent<SphereCollider>().enabled = false;
    //             PlayerScript.instance.containerHit.transform.parent.transform.parent.GetComponent<PedestalScript>().itemOnSelfScript = selectedItem.GetComponent<ItemScript>();
    //             PlayerScript.instance.containerHit.transform.parent.transform.parent.GetComponent<PedestalScript>().ItemPlaced();
    //         }
    //         else
    //         {
    //             PlayerScript.instance.containerHit.transform.parent.transform.parent.GetComponent<PedestalScript>().ItemRemoved();
    //         }
    //     }


    //     // For item placing; does proper item placing depending upon if theres an item in the container or not
    //     public void ItemInteract()
    //     {
    //         ItemScript itemInContainer = PlayerScript.instance.containerHit.transform.parent.GetComponentInChildren<ItemScript>();
    //         if (itemInContainer != null)
    //         {
    //             AddItemToContainer();
    //             DeselectAll();
    //             GetItems();
    //             PickUpItem(itemInContainer.gameObject);
    //         }
    //         else 
    //         {
    //             AddItemToContainer();
    //         }
    //         DeselectAll();
    //         GetItems();
    //     }
}
