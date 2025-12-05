using UnityEngine;
using System.Threading.Tasks;

public class PotionCraftAnimScript : MonoBehaviour
{
    Animator animator;


    public async Task CraftAnim()
    {
        animator.SetTrigger("Crafting");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }
}
