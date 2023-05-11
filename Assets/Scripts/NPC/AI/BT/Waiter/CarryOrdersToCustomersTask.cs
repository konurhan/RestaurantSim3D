using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CarryOrdersToCustomersTask : Node
{
    private Waiter waiter;
    private NavMeshAgent agent;
    public CarryOrdersToCustomersTask(Waiter waiter)
    {
        this.waiter = waiter;
        agent = waiter.GetComponent<NavMeshAgent>();
    }

    public override NodeState Evaluate()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    Debug.Log("waiter arrived to a customer");
                    KeyValuePair<GameObject, Customer> prevOrder = waiter.inventory.First();
                    waiter.DeliverOrder(prevOrder.Key, prevOrder.Value);
                    //waiter.inventory.Remove(prevOrder.Key);//removing the customer just served
                    
                    if (waiter.inventory.Count > 0)
                    {
                        KeyValuePair<GameObject,Customer> order = waiter.inventory.First();
                        agent.SetDestination(order.Value.seatTransform.position);
                        nodeState = NodeState.RUNNING;
                        return nodeState;
                    }
                    else//delivered all orders in the inventory
                    {
                        waiter.pickedUpOrders = false;
                        waiter.delivering = false;
                        nodeState = NodeState.SUCCEED;
                        return nodeState;
                    }
                }
            }
        }
        nodeState = NodeState.RUNNING;
        return nodeState;
    }
}
