using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarryOrdersToReadyCounterTask : Node
{
    Cook cook;
    NavMeshAgent agent;
    Animator animator;
    public CarryOrdersToReadyCounterTask(Cook cook)
    {
        this.cook = cook;
        agent = cook.gameObject.GetComponent<NavMeshAgent>();
        animator = cook.gameObject.GetComponent<Animator>();
    }
    public override NodeState Evaluate()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    animator.SetBool("isCarrying", false);
                    Debug.Log("Cook arrived to readycounter");
                    RestaurantManager.Instance.AddObjectToReadyCounter(cook.preparedRecipe);
                    RestaurantManager.Instance.readyQueue.Enqueue(new KeyValuePair<GameObject, Customer>(cook.preparedRecipe, cook.currentOrder.orderer));
                    cook.delivering = false;
                    cook.preparedRecipe = null;
                    cook.currentOrder = null;
                    nodeState = NodeState.SUCCEED;
                    return nodeState;
                }
            }
        }
        nodeState = NodeState.RUNNING;
        return nodeState;
    }
}
