using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckIfShouldLeave : Node
{
    Customer customer;
    NavMeshAgent agent;
    Animator animator;
    public CheckIfShouldLeave(Customer customer)
    {
        this.customer = customer;
        agent = customer.GetComponent<NavMeshAgent>();
        animator = customer.gameObject.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        if (customer.Thirst > 80 && customer.Hunger > 80)
        {
            customer.seatTransform.gameObject.GetComponent<SeatController>().LeaveTheSeat();//test if this operation is succesful
            agent.SetDestination(RestaurantManager.Instance.RestaurantComponents.GetChild(2).position);

            if (!customer.isLeaving)
            {
                customer.isLeaving = true;
                RestaurantManager.Instance.satisfiedCustomers++;
                Debug.Log("Customer is leaving satisfied");//might get caled more than once

                animator.SetBool("isSitting", false);
            }
            //CustomerArrivalManager.Instance.CheckForEndOfTheDay();

            nodeState = NodeState.SUCCEED;
            return nodeState;
        }
        else
        {
            customer.isWaitingToOrder = true;
            customer.isOrdered = false;
            customer.isWaitingToEatDrink = true;
            customer.isCalledWaiter = false;
            customer.isOrderArrived = false;
            nodeState = NodeState.FAILED;
            return nodeState;
        }
    }
}
