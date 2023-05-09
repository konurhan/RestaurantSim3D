using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfShouldLeave : Node
{
    Customer customer;
    public CheckIfShouldLeave(Customer customer)
    {
        this.customer = customer;
    }

    public override NodeState Evaluate()
    {
        if (customer.Thirst > 80 && customer.Hunger > 80)
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
