using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToCustomerTask : Node
{
    Waiter waiter;
    Customer customer;
    NavMeshAgent agent;
    Animator animator;
    public GoToCustomerTask(Waiter waiter)
    {
        this.waiter = waiter;
        agent = waiter.gameObject.GetComponent<NavMeshAgent>();
        animator = waiter.gameObject.GetComponent<Animator>();
    }
    public override NodeState Evaluate()
    {
        //What happens if customer leaves, our reference will become null???????????????????????????????????????

        if(waiter.currentCustomer == null)//if waiter has no current customers, i.e. customer has left, abort operation
        {
            animator.SetBool("isWalking", false);
            nodeState = NodeState.FAILED;
            return nodeState;
        }
        customer = waiter.currentCustomer;

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    animator.SetBool("isWalking", false);
                    Debug.Log("waiter arrived to his customer");
                    customer.isWaitingToOrder = false;
                    waiter.currentCustomer = null;
                    nodeState = NodeState.SUCCEED;
                    return nodeState;
                }
            }
        }
        nodeState = NodeState.RUNNING;
        return nodeState;
    }
}
