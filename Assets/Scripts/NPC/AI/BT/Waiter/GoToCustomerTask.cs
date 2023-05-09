using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToCustomerTask : Node
{
    Waiter waiter;
    Customer customer;
    NavMeshAgent agent;
    public GoToCustomerTask(Waiter waiter)
    {
        this.waiter = waiter;
        agent = waiter.gameObject.GetComponent<NavMeshAgent>();
    }
    public override NodeState Evaluate()
    {
        //What happens if customer leaves, our reference will become null???????????????????????????????????????

        if(waiter.currentCustomer == null)//if waiter has no current customers, i.e. customer has left, abort operation
        {
            nodeState = NodeState.FAILED;
            return nodeState;
        }
        customer = waiter.currentCustomer;

        /*if (!agent.hasPath && !agent.pathPending)//agent has no path and no path is currently being calculated
        {
            if ((agent.transform.position - customer.seatTransform.position).magnitude <= agent.stoppingDistance)
            {//if agent is reached to its target and stopped
                customer.isWaitingToOrder = false;
                nodeState = NodeState.SUCCEED;
                return nodeState;
            }
            else//if agent has no destination set
            {
                agent.SetDestination(customer.seatTransform.position);
                nodeState = NodeState.RUNNING;
                return nodeState;
            }
        }
        else//agent currently has a destination
        {
            nodeState = NodeState.RUNNING;
            return nodeState;
        }*/

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    Debug.Log("waiter arrived to his customer");
                    customer.isWaitingToOrder = false;
                    nodeState = NodeState.SUCCEED;
                    return nodeState;
                }
            }
        }
        nodeState = NodeState.RUNNING;
        return nodeState;
    }
}
