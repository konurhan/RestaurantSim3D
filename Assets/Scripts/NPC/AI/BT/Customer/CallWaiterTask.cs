using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallWaiterTask : Node
{
    Customer customer;
    public CallWaiterTask(Customer customer)
    {

        this.customer = customer;

    }

    public override NodeState Evaluate()
    {
        if (!customer.isCalledWaiter)//if waiter is not called
        {
            customer.isCalledWaiter = true;
            RestaurantManager.Instance.orderRequestQueue.Enqueue(customer.gameObject);
        }
        nodeState = NodeState.SUCCEED;
        return nodeState;
    }
}
