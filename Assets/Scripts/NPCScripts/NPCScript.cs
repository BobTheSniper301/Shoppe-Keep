using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class NPCScipt : MonoBehaviour
{
    NPCAnimationScript npcAnimationScript;

    NavMeshAgent agent;

    public GameObject target;

    int lastWentToEnd;

    int walkOrBuy;

    bool isTargetAPedestal;

    public async Task Purchase()
    {
        if (target.transform.parent.root.GetComponentInChildren<ItemScript>() == null)
        {
            return;
        }

        await npcAnimationScript.PlayerPurchaseAnim();
        Debug.Log("purchase");
        GameControllerScript.instance.pedestals.Remove(target);
        isTargetAPedestal = false;

    }


    public void DetermineTarget()
    {

        // (x, y) makes a random number from x to y-1 
        walkOrBuy = Random.Range(0, 4);

        // 1/3 chance to go buy instead of walk
        if (walkOrBuy != 3)
        {
            // Debug.Log("walkway");
            target = GameControllerScript.instance.walkways[lastWentToEnd];
        }
        else if (GameControllerScript.instance.pedestals.Count > 0)
        {
            // Debug.Log("find pedestal");
            target = GameControllerScript.instance.pedestals[Random.Range(0, GameControllerScript.instance.pedestals.Count)];
            isTargetAPedestal = true;
        }

        if (target != null)
        {
            agent.SetDestination(target.transform.position);
        }

        agent.isStopped = false;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();

        npcAnimationScript = GetComponent<NPCAnimationScript>();

    }

    // Update is called once per frame
    async void Update()
    {
        
        if (Vector3.Distance(this.transform.position, agent.destination) < 2.5f && agent.isStopped == false)
        {
            // To swap between start and end on path finding
            if (walkOrBuy == 0)
            {
                lastWentToEnd = 1;
            }
            else if (walkOrBuy == 1)
            {
                lastWentToEnd = 0;
            }


            agent.isStopped = true;

            this.transform.rotation = target.transform.rotation;
            
            if (isTargetAPedestal)
            {
                await Purchase();
            }

            DetermineTarget();
        }
            
    }
}
