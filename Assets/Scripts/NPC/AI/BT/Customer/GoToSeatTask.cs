using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToSeatTask : Node
{
    Customer customer;
    NavMeshAgent agent;
    public GoToSeatTask(Customer customer)
    {
        this.customer = customer;
        Debug.Log("agent gameObject name: " + customer.transform.gameObject.name);
        agent = customer.gameObject.GetComponent<NavMeshAgent>();
    }

    public override NodeState Evaluate()
    {
        /*if(!agent.hasPath && !agent.pathPending)//agent has no path and no path is currently being calculated
        {
            Vector3 distance = agent.transform.position - customer.seatTransform.position;
            Vector2 distXZ = new Vector2(distance.x, distance.z);
            Debug.Log(distXZ.magnitude);
            Debug.Log(agent.remainingDistance);
            if ((distXZ).magnitude <= agent.remainingDistance){//if agent is reached to its target and stopped
                customer.isSeated = true;
                nodeState = NodeState.SUCCEED;
                return nodeState;
            }
            else//if agent has no destination set
            {
                Debug.Log("Setting customer agent's destination to a seat");
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
