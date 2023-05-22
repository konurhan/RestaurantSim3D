using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CurrentCustomerCheck : Node
{
    Waiter waiter;
    Animator animator;
    public CurrentCustomerCheck(Waiter waiter)
    {
        this.waiter = waiter;
        animator = waiter.gameObject.GetComponent<Animator>();
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
            while(waiter.currentCustomer==null && RestaurantManager.Instance.orderRequestQueue.Count > 0)
            {
                GameObject customer = RestaurantManager.Instance.orderRequestQueue.Dequeue();
                if (customer == null)//check if dequed object is null
                {
                    waiter.currentCustomer = null;
                }
                else
                {
                    waiter.currentCustomer = customer.GetComponent<Customer>();
                }
            }
            if(waiter.currentCustomer!= null)
            {
                animator.SetBool("isWalking", true);
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
        else
        {
            nodeState = NodeState.FAILED;
            return nodeState;
        }
    }
}
