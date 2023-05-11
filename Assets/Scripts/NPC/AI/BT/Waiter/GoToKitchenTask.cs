using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//we should use a navmesh agent here
public class GoToKitchenTask : Node
{
    Waiter waiter;
    NavMeshAgent agent;
    public GoToKitchenTask(Waiter waiter)
    {
        this.waiter = waiter;
        agent = waiter.gameObject.GetComponent<NavMeshAgent>();
    }
    public override NodeState Evaluate()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    Debug.Log("waiter arrived to kitchen to take-on orders");
                    nodeState = NodeState.SUCCEED;
                    return nodeState;
                }
            }
        }
        nodeState = NodeState.RUNNING;
        return nodeState;
    }
}
