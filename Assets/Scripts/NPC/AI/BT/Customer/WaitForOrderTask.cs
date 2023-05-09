using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForOrderTask : Node
{
    Customer customer;
    public WaitForOrderTask(Customer customer)
    {
        this.customer = customer;
    }
    public override NodeState Evaluate()
    {
        if (!customer.isOrderArrived)
        {
            customer.Patience--;
            if (customer.Patience <= 0)
            {
                customer.isLeaving = true;
                nodeState = NodeState.FAILED;
                return nodeState;
            }
            else
            {
                Debug.Log("Customer is waiting for order");
                nodeState = NodeState.RUNNING;
                return nodeState;
            }
        }
        else
        {
            nodeState = NodeState.SUCCEED;
            return nodeState;
        }
    }
}
