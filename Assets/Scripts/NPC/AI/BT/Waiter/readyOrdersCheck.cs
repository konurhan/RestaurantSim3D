using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class readyOrdersCheck : Node
{
    Waiter waiter;
    Animator animator;
    public readyOrdersCheck(Waiter waiter)
    {
        this.waiter = waiter;
        animator = waiter.gameObject.GetComponent<Animator>();
    }
    public override NodeState Evaluate()
    {
        if(RestaurantManager.Instance.readyQueue.Count == 0 && !waiter.delivering)
        {
            nodeState = NodeState.FAILED;
            return nodeState;
        }
        else if(!waiter.delivering)
        {
            waiter.delivering = true;
            waiter.gameObject.GetComponent<NavMeshAgent>().SetDestination(RestaurantManager.Instance.RestaurantComponents.GetChild(5).transform.position);//send waiter to kitchen to fetch the order
            animator.SetBool("isWalking", true);
            nodeState = NodeState.SUCCEED;
            return nodeState;
        }
        else
        {
            nodeState = NodeState.SUCCEED;
            return nodeState;
        }
    }
}
