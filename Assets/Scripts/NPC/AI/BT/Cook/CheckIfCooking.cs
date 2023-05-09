using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfCooking : Node
{
    Cook cook;
    public CheckIfCooking(Cook cook)
    {
        this.cook = cook;
    }
    public override NodeState Evaluate()
    {
        if (!cook.cooking)
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
