using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaitForWaiterToTakeOrderTask : Node
{
    Customer customer;
    NavMeshAgent agent;
    public WaitForWaiterToTakeOrderTask(Customer customer)
    {
        this.customer = customer;
        agent = customer.GetComponent<NavMeshAgent>();
    }
    public override NodeState Evaluate()//implement after A*
    {
        if (customer.isWaitingToOrder)
        {
            customer.Patience--;
            if (customer.Patience <= 0)
            {
                //bu iki satırı test et
                customer.seatTransform.gameObject.GetComponent<SeatController>().LeaveTheSeat();//test if this operation is succesful
                agent.SetDestination(RestaurantManager.Instance.RestaurantComponents.GetChild(2).position);

                if (!customer.isLeaving)
                {
                    customer.isLeaving = true;
                    RestaurantManager.Instance.angryCustomers++;
                    Debug.Log("Customer is leaving angry, order was late");
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
