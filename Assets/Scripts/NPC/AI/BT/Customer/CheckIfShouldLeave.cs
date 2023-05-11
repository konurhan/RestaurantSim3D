using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckIfShouldLeave : Node
{
    Customer customer;
    NavMeshAgent agent;
    public CheckIfShouldLeave(Customer customer)
    {
        this.customer = customer;
        agent = customer.GetComponent<NavMeshAgent>();
    }

    public override NodeState Evaluate()
    {
        if (customer.Thirst > 80 && customer.Hunger > 80)
        {
            customer.seatTransform.gameObject.GetComponent<SeatController>().LeaveTheSeat();//test if this operation is succesful
            agent.SetDestination(RestaurantManager.Instance.RestaurantComponents.GetChild(2).position);

            RestaurantManager.Instance.satisfiedCustomers++;
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
