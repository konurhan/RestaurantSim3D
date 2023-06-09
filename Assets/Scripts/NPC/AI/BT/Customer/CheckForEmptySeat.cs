using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckForEmptySeat : Node
{
    Customer customer;
    Animator animator;
    public CheckForEmptySeat(Customer customer)
    {
        this.customer = customer;
        animator = customer.gameObject.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        if(customer.seatTransform != null)
        {
            nodeState = NodeState.SUCCEED;
            return nodeState;
        }
       
        foreach(GameObject seat in RestaurantManager.Instance.seats)
        {
            if (seat.GetComponent<SeatController>().GetOccupant() == null)
            {
                seat.GetComponent<SeatController>().SetOccupant(customer.gameObject);
                customer.seatTransform = seat.transform;
                customer.gameObject.GetComponent<NavMeshAgent>().SetDestination(seat.transform.position);
                animator.SetBool("isWalking",true);
                nodeState = NodeState.SUCCEED;
                return nodeState;
            }
        }

        if (!customer.isLeaving)
        {
            customer.isLeaving = true;
            Debug.Log("customer is denied");
            RestaurantManager.Instance.deniedCustomers++;
        }
        //CustomerArrivalManager.Instance.CheckForEndOfTheDay();

        nodeState = NodeState.FAILED;
        return nodeState;
    }
}
