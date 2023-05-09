using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfSeated : Node
{
    Customer customer;
    public CheckIfSeated(Customer customer)
    {
        this.customer = customer;
    }
    public override NodeState Evaluate()
    {
        if (customer.isSeated)
        {
            nodeState =NodeState.SUCCEED;
            return nodeState;
        }
        else
        {
            nodeState =NodeState.FAILED;
            return nodeState;
        }
    }
}
