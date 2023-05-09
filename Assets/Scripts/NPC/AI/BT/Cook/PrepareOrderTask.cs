using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PrepareOrderTask : Node
{
    Cook cook;
    public PrepareOrderTask(Cook cook)
    {
        this.cook = cook;
    }
    public override NodeState Evaluate()
    {
        if(cook.preperationTime > 0)
        {
            cook.preperationTime--;
            //play cooking animation here
            nodeState = NodeState.RUNNING;
            return nodeState;
        }
        else
        {
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
