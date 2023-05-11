using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToSeatTask : Node
{
    Customer customer;
    NavMeshAgent agent;
    public GoToSeatTask(Customer customer)
    {
        this.customer = customer;
        Debug.Log("agent gameObject name: " + customer.transform.gameObject.name);
        agent = customer.gameObject.GetComponent<NavMeshAgent>();
    }

    public override NodeState Evaluate()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    Debug.Log("Customer arrived to his seat");
                    customer.isSeated = true;
                    nodeState = NodeState.SUCCEED;
                    return nodeState;
                }
            }
        }
        nodeState = NodeState.RUNNING;
        return nodeState;
    }
}
