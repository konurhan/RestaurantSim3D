using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isNotDeliveringCheck : Node
{
    Waiter waiter;
    public isNotDeliveringCheck(Waiter waiter)
    {

        this.waiter = waiter;

    }

    public override NodeState Evaluate()
    {
        if (waiter.delivering)
        {
            nodeState = NodeState.FAILED;
            return nodeState;
        }
        else
        {
            nodeState = NodeState.SUCCEED;
            return nodeState;
        }
    }
}
