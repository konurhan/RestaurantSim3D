using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfOrdered : Node
{
    Customer customer;
    public CheckIfOrdered(Customer customer)
    {
        this.customer = customer;
    }
    public override NodeState Evaluate()
    {
        if (customer.isOrdered)
        {
            nodeState = NodeState.SUCCEED;
            return nodeState;
        }
        else
        {
            nodeState = NodeState.FAILED;
            return nodeState;
        }
    }
}
