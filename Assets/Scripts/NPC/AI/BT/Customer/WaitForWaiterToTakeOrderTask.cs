using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForWaiterToTakeOrderTask : Node
{
    Customer customer;
    public WaitForWaiterToTakeOrderTask(Customer customer)
    {
        this.customer = customer;
    }
    public override NodeState Evaluate()//implement after A*
    {
        if (customer.isWaitingToOrder)
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
