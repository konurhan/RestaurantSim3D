using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CurrentCustomerCheck : Node
{
    Waiter waiter;
    public CurrentCustomerCheck(Waiter waiter)
    {
        this.waiter = waiter;
    }
    public override NodeState Evaluate()
    {
        if(waiter.currentCustomer == null && RestaurantManager.Instance.orderRequestQueue.Count == 0)
        {
            nodeState = NodeState.FAILED; 
            return nodeState;
        }
        else if(waiter.currentCustomer != null)
        {
            nodeState = NodeState.SUCCEED;
            return nodeState;
        }
        else if(waiter.currentCustomer == null && RestaurantManager.Instance.orderRequestQueue.Count > 0)
        {
            waiter.currentCustomer = RestaurantManager.Instance.orderRequestQueue.Dequeue().GetComponent<Customer>();
            waiter.gameObject.GetComponent<NavMeshAgent>().SetDestination(waiter.currentCustomer.seatTransform.position);
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
