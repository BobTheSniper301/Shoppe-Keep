using UnityEngine;

public class PotionCraftAnimScript : MonoBehaviour
{
    Animator animator;


    public void CraftAnim()
    {
        animator.SetTrigger("Crafting");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Soley for the animation event
    void _CraftItem()
    {
        UiManager.instance.craftingMenu.GetComponent<CraftingMenuScript>().CraftItem();
    }
}
