using System.Threading.Tasks;
using UnityEngine;

public class NPCAnimationScript : MonoBehaviour
{
    Animator animator;


    public async Task PlayerPurchaseAnim()
    {
        animator.SetTrigger("NPCPurchaseTrigger");
        // Wait for half of the animation to finish
        await Task.Delay(750);   
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

}
