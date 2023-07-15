using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaitForWaiterToTakeOrderTask : Node
{
    Customer customer;
    NavMeshAgent agent;
    Animator animator;
    public WaitForWaiterToTakeOrderTask(Customer customer)
    {
        this.customer = customer;
        agent = customer.GetComponent<NavMeshAgent>();
        animator = customer.gameObject.GetComponent<Animator>();
    }
    public override NodeState Evaluate()
    {
        if (customer.isWaitingToOrder)
        {
            customer.Patience--;
            if (customer.Patience <= 0)
            {
                customer.seatTransform.gameObject.GetComponent<SeatController>().LeaveTheSeat();
                agent.SetDestination(RestaurantManager.Instance.RestaurantComponents.GetChild(2).position);

                if (!customer.isLeaving)
                {
                    customer.isLeaving = true;
                    RestaurantManager.Instance.angryCustomers++;
                    Debug.Log("Customer is leaving angry, couldn't make the order");

                    animator.SetBool("isSitting", false);
                    //customer.gameObject.transform.position -= Vector3.up / 4;
                }

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
