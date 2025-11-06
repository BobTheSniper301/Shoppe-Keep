using UnityEngine;
using UnityEngine.AI;

public class NPCScipt : MonoBehaviour
{
    [SerializeField] GameObject pedestal;
    NavMeshAgent agent;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(pedestal.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log("dist " + Vector3.Distance(this.transform.position, agent.destination));
        //Debug.Log("stopping dis " +agent.stoppingDistance);
        //if (Vector3.Distance(this.transform.position, agent.destination) < 2.1f)
        //{
        //    agent.velocity = Vector3.zero;
        //    agent.isStopped = true;
        //    Debug.Log(agent.isStopped);
        //}
    }
}
