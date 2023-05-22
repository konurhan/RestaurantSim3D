using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LeaveRestaurant : Node
{
    Customer customer;
    NavMeshAgent agent;
    Animator animator;
    public LeaveRestaurant(Customer customer)
    {
        this.customer = customer;
        agent = customer.GetComponent<NavMeshAgent>();
        animator = customer.gameObject.GetComponent<Animator>();
    }
    public override NodeState Evaluate()
    {
        
        
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    animator.SetBool("isWalking", false);
                    if(customer.seatTransform != null)
                    {
                        customer.seatTransform = null;
                        customer.Leave();
                        Debug.Log("customer leave is just called");
                        CustomerArrivalManager.Instance.CheckForEndOfTheDay();
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
