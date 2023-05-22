using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaitForOrderTask : Node
{
    Customer customer;
    NavMeshAgent agent;
    Animator animator;
    public WaitForOrderTask(Customer customer)
    {
        this.customer = customer;
        agent = customer.GetComponent<NavMeshAgent>();
        animator = customer.gameObject.GetComponent<Animator>();
    }
    public override NodeState Evaluate()
    {
        if (!customer.isOrderArrived)
        {
            customer.Patience--;
            if (customer.Patience <= 0)
            {
                customer.seatTransform.gameObject.GetComponent<SeatController>().LeaveTheSeat();//test if this operation is succesful
                agent.SetDestination(RestaurantManager.Instance.RestaurantComponents.GetChild(2).position);

                if (!customer.isLeaving)
                {
                    customer.isLeaving = true;
                    RestaurantManager.Instance.angryCustomers++;
                    Debug.Log("Customer is leaving angry, order was late");//called more than once

                    animator.SetBool("isSitting", false);
                }
                //CustomerArrivalManager.Instance.CheckForEndOfTheDay();

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
            //Debug.Log("Customer gets the order");
            nodeState = NodeState.SUCCEED;
            return nodeState;
        }
    }
}
