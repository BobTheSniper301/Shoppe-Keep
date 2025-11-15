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

    [SerializeField] bool isTargetAPedestal;


    // Will have npc handle all of the purchasing due to me wanting them to grow stronger or have them know related things in the future so it would be easier for itself to know what items it has bought before
    public async Task Purchase()
    {
        PedestalScript pedestalScript = target.transform.parent.root.GetComponent<PedestalScript>();
        ItemScript pedestalItem = pedestalScript.itemOnSelfScript;

        Debug.Log("pedestal item " + pedestalItem);
        Debug.Log("pedestal script " + pedestalScript);
        Debug.Log("player gold " + PlayerScript.instance.playerData.gold);
        Debug.Log("item price " + pedestalItem.itemData.price);

        await npcAnimationScript.PlayerPurchaseAnim();
        Debug.Log("purchase");

        PlayerScript.instance.playerData.gold += pedestalItem.itemData.price; 
        pedestalScript.ItemRemoved();
        Destroy(pedestalItem.gameObject);

        GameControllerScript.itemSale?.Invoke();
        

        

    }


    public void DetermineTarget()
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
            // Removes the pedestal from the list so it doesn't get targeted by anything else
            GameControllerScript.instance.pedestals.Remove(target);
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

        agent.SetDestination(target.transform.position);

        agent.updateRotation = true;
    }

    // Update is called once per frame
    async void Update()
    {
        // Triggers when the NPC has reached the locations
        if (Vector3.Distance(this.transform.position, agent.destination) <= 2.4f && agent.isStopped == false) // dist <= Agent radius
        {
            Debug.Log("stop");
            agent.isStopped = true;
            agent.velocity = Vector3.zero;

            this.transform.rotation = target.transform.rotation;


            if (isTargetAPedestal && target.transform.parent.root.GetComponent<PedestalScript>().itemOnSelfScript != null)
            {
                await Purchase();
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
