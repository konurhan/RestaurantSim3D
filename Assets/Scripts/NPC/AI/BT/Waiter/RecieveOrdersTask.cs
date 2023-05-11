using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.Port;

public class RecieveOrdersTask : Node
{
    Waiter waiter;
    NavMeshAgent agent;
    public RecieveOrdersTask(Waiter waiter)
    {
        this.waiter = waiter;
        agent = waiter.GetComponent<NavMeshAgent>();
    }

    public override NodeState Evaluate()//this method doesn't work properly, if once succeded it always return succeed
    {
        if(!waiter.pickedUpOrders)
        {
            waiter.pickedUpOrders = true;
            waiter.PickUpReadyOrders();
            agent.SetDestination(waiter.inventory.First().Value.seatTransform.position);//setting the destination for the first customer
        }
        nodeState = NodeState.SUCCEED;
        return nodeState;
    }
}
