using UnityEngine;
using UnityEngine.AI;

public class NPCScipt : MonoBehaviour
{

    NavMeshAgent agent;

    public GameObject target = GameControllerScript.instance.walkways[0];

    int lastWentToEnd;

    int walkOrBuy;


    public void Purchase()
    {
        Debug.Log("purchase");
    }


    void DetermineTarget()
    {
        if (target != GameControllerScript.instance.walkways[0] && target != GameControllerScript.instance.walkways[1])
        {
            Purchase();
        }

        agent.isStopped = false;

        walkOrBuy = Random.Range(0, 2);

        if (walkOrBuy == 0)
        {
            target = GameControllerScript.instance.walkways[lastWentToEnd];
        }
        else if (GameControllerScript.instance.pedestals.Count > 0)
        {
            target = GameControllerScript.instance.pedestals[Random.Range(0, GameControllerScript.instance.pedestals.Count)];
        }

        if (target != null)
        {
            agent.SetDestination(target.transform.position);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();

        DetermineTarget();

    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log("stopping dis " + agent.stoppingDistance);
        if (Vector3.Distance(this.transform.position, agent.destination) < 2.1f)
        {
            Debug.Log("stop");
            if (walkOrBuy == 0)
            {
                lastWentToEnd = 1;
            }
            
            agent.isStopped = true;
            this.transform.rotation = target.transform.rotation;
            DetermineTarget();
        }
            
    }
}
