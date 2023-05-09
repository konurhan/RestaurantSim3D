using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readyOrdersCheck : Node
{
    Waiter waiter;
    public readyOrdersCheck(Waiter waiter)
    {
        this.waiter = waiter;
    }
    public override NodeState Evaluate()
    {
        if(RestaurantManager.Instance.readyQueue.Count == 0 && !waiter.delivering)
        {
            nodeState = NodeState.FAILED;
            return nodeState;
        }
        else
        {
            waiter.delivering = true;
            nodeState = NodeState.SUCCEED;
            return nodeState;
        }
    }
}
