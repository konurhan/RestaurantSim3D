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

                RestaurantManager.Instance.angryCustomers++;
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
            Debug.Log("Customer gets the order");
            nodeState = NodeState.SUCCEED;
            return nodeState;
        }
    }
}
