using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LeaveRestaurant : Node
{
    Customer customer;
    NavMeshAgent agent;
    public LeaveRestaurant(Customer customer)
    {
        this.customer = customer;
        agent = customer.GetComponent<NavMeshAgent>();
    }
    public override NodeState Evaluate()
    {
        
        
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    if(customer.seatTransform != null)
                    {
                        customer.seatTransform = null;
                        customer.Leave();
                    }
                    nodeState = NodeState.SUCCEED;
                    return nodeState;
                }
            }
        }
        nodeState = NodeState.RUNNING;
        return nodeState;
    }
}
