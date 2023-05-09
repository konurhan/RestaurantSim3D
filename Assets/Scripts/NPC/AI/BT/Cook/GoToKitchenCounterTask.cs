using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToKitchenCounterTask : Node
{
    Cook cook;
    NavMeshAgent agent;
    public GoToKitchenCounterTask(Cook cook)
    {
        this.cook = cook;
        agent = cook.gameObject.GetComponent<NavMeshAgent>();
    }
    public override NodeState Evaluate()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    Debug.Log("Cook arrived to kitchencounter");
                    nodeState = NodeState.SUCCEED;
                    return nodeState;
                }
            }
        }
        nodeState = NodeState.RUNNING;
        return nodeState;
    }
}
