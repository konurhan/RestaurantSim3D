using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PrepareOrderTask : Node
{
    Cook cook;
    Animator animator;
    public PrepareOrderTask(Cook cook)
    {
        this.cook = cook;
        animator = cook.gameObject.GetComponent<Animator>();
    }
    public override NodeState Evaluate()
    {
        if(cook.preperationTime > 0)
        {
            cook.preperationTime-=2;
            //play cooking animation here
            nodeState = NodeState.RUNNING;
            return nodeState;
        }
        else
        {
            animator.SetBool("isCooking", false);
            animator.SetBool("isCarrying", true);
            cook.cooking = false;
            cook.delivering = true;
            cook.preperationTime = 0;
            cook.PrepareOrder(cook.currentOrder);
            //cook.currentOrder = null;
            cook.gameObject.GetComponent<NavMeshAgent>().SetDestination(RestaurantManager.Instance.RestaurantComponents.GetChild(5).gameObject.transform.position);
            nodeState = NodeState.SUCCEED;
            return nodeState;
        }
    }
}
