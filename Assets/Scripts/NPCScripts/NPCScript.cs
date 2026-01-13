using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NPCScipt : MonoBehaviour
{
    NPCAnimationScript npcAnimationScript;

    NavMeshAgent agent;

    public GameObject target;

    bool lastWentToEnd;

    int walkOrBuy;

    [SerializeField] bool isTargetAPedestal;

    int marketValue = 20;
    int spendingLimit = 17;


    // Used to have npc handle all of purchase but instead is now on the pedestal. This function is for ease of use and for the await for animation
    public async Task Purchase()
    {
        await npcAnimationScript.PlayerPurchaseAnim();
        target.transform.parent.root.GetComponent<PedestalScript>().PurchaseItem();

        Debug.Log("done enough with anim");
    }


    bool CanAffordItem()
    {
        // implement a function that gets acess to a dictionary to find the market value of an item and then compare it
        if (target.transform.parent.root.GetComponent<PedestalScript>().itemPrice <= marketValue && target.transform.parent.root.GetComponent<PedestalScript>().itemPrice <= spendingLimit)
        {
            return(true);
        }
        else
        {
            return(false);
        }
    }


    public void DetermineTarget()
    {
        // To swap between start and end on path finding
        lastWentToEnd = !lastWentToEnd;
            
        // (x, y) makes a random number from x+1 to y-1 so (0,4) gives 1,2,3
        walkOrBuy = Random.Range(0, 4);

        // Debug.Log("walk or buy: " + walkOrBuy);

        // TODO: Current temp change to 2/3, change back later
        // 1/3 chance to go buy instead of walk
        if (walkOrBuy < 2)
        {
            // Debug.Log("walkway");
            target = AiManager.instance.walkways[Convert.ToInt32(lastWentToEnd)];
        }
        else if (AiManager.instance.pedestals.Count > 0)
        {
            target = AiManager.instance.pedestals[Random.Range(0, AiManager.instance.pedestals.Count)];
            // Removes the pedestal from the list so it doesn't get targeted by anything else
            AiManager.instance.pedestals.Remove(target);
            isTargetAPedestal = true;
        }

        if (target != null)
        {
            agent.SetDestination(target.transform.position);
        }

        agent.isStopped = false;
    }

    void DoubleCheckItem()
    {
        if (isTargetAPedestal && target.transform.parent.root.GetComponent<PedestalScript>().placedItemScript == null)
        {
            isTargetAPedestal = false;
            DetermineTarget();
        }
    }




    void OnEnable()
    {
        EventManager.pedestalChanged += DoubleCheckItem;
    }
    void OnDisable()
    {
        EventManager.pedestalChanged -= DoubleCheckItem;
    }


    void Start()
    {

        agent = GetComponent<NavMeshAgent>();

        npcAnimationScript = GetComponent<NPCAnimationScript>();

        //  TODO? Hardcoded because bugs out if not (must set first target in editor)
        agent.SetDestination(target.transform.position);

        agent.updateRotation = true;
    }


    async void Update()
    {
        // Triggers when the NPC has reached the locations
        if (Vector3.Distance(this.transform.position, agent.destination) <= 2.4f && agent.isStopped == false) // dist <= Agent radius
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;

            this.transform.rotation = target.transform.rotation;


            if (isTargetAPedestal && target.transform.parent.root.GetComponent<PedestalScript>().placedItemScript != null)
            {
                if (CanAffordItem())
                {
                    await Purchase();
                }
                else
                {
                    // Debug.Log("too expensive");
                    AiManager.instance.pedestals.Add(target);
                }
            }
            isTargetAPedestal = false;

            DetermineTarget();
        }
        // TODO: Learn to smoothly rotate the NPC when it reaches the location
        // if (agent.velocity.magnitude <= 0.1f)
        // {
        //     Quaternion.RotateTowards(this.transform.rotation, target.transform.rotation, 1);
        // }
    }
}
