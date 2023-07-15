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
                    //customer.gameObject.transform.localPosition += Vector3.up ;
                    Debug.Log("Customer arrived to his seat");
                    customer.isSeated = true;
                    //agent.updateRotation = false;
                    int lookAt = customer.seatTransform.GetSiblingIndex();
                    if(lookAt == 0 ||  lookAt == 1)//look towards the enterance
                    {
                        customer.gameObject.transform.eulerAngles = Vector3.zero;
                        Debug.Log("setting customer lookat");
                    }
                    else//look towards the kitchen
                    {
                        customer.gameObject.transform.eulerAngles = new Vector3(0,180,0);
                    }
                    //agent.updateRotation = true;
                    nodeState = NodeState.SUCCEED;
                    return nodeState;
                }
            }
        }
        nodeState = NodeState.RUNNING;
        return nodeState;
    }
}
