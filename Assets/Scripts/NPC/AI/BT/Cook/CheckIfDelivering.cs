using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfDelivering : Node
{
    Cook cook;
    public CheckIfDelivering(Cook cook)
    {
        this.cook = cook;
    }
    public override NodeState Evaluate()
    {
        if (!cook.delivering)
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
