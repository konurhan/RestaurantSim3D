using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToSeatTask : Node
{
    Customer customer;
    NavMeshAgent agent;
    Animator animator;
    public GoToSeatTask(Customer customer)
    {
        this.customer = customer;
        //Debug.Log("agent gameObject name: " + customer.transform.gameObject.name);
        agent = customer.gameObject.GetComponent<NavMeshAgent>();
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
                    animator.SetBool("isSitting", true);

                    Debug.Log("Customer arrived to his seat");
                    customer.isSeated = true;
                    nodeState = NodeState.SUCCEED;
                    return nodeState;
                }
            }
        }
        nodeState = NodeState.RUNNING;
        return nodeState;
    }
}
