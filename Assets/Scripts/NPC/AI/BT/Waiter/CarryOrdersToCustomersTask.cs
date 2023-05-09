using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarryOrdersToCustomersTask : Node
{
    private Waiter waiter;
    public CarryOrdersToCustomersTask(Waiter waiter)
    {
        this.waiter = waiter;
    }

    public override NodeState Evaluate()
    {
        // examine this method, here we could seperate going to customer and delivering the order into seperate tasks
        if(waiter.inventory.Count > 0)
        {
            KeyValuePair<GameObject, Customer> pair = waiter.inventory.First();
            GameObject order = pair.Key;
            Customer orderer = pair.Value;
            //put the following navigation operation in an if statement so that on each tick the waiter partially moves
            waiter.GoToDeliverOrder(new Vector2(orderer.transform.position.x, orderer.transform.position.y));
            //
            waiter.DeliverOrder(order, orderer);

            nodeState = NodeState.RUNNING;
            return nodeState;
        }
        else
        {
            nodeState = NodeState.SUCCEED;
            return nodeState;
        }
    }
}
