using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOrderTask : Node
{
    Waiter waiter;
    Customer customer;
    public AddOrderTask(Waiter waiter)
    {
        this.waiter = waiter;
    }
    public override NodeState Evaluate()
    {
        
        //signal customer to give order, customer method will add the order to list

        if(customer == null)
        {
            nodeState = NodeState.FAILED;//customer has left before we could take his order
            return nodeState;
        }
        else
        {
            customer = waiter.currentCustomer;
            waiter.currentCustomer = null;//customer order is successfully taken
            nodeState = NodeState.SUCCEED;
            return nodeState;
        }
    }
}
