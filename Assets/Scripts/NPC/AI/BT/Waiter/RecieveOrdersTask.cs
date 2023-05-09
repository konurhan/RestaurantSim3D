using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

public class RecieveOrdersTask : Node
{
    Waiter waiter;
    public RecieveOrdersTask(Waiter waiter)
    {
        this.waiter = waiter;
    }

    public override NodeState Evaluate()//this method doesn't work properly, if once succeded it always return succeed
    {
        if (waiter.inventory.Count < waiter.capacity)
        {
            for(int i=0; i<waiter.capacity- waiter.inventory.Count; i++)
            {
                KeyValuePair<GameObject, Customer> pair = RestaurantManager.Instance.readyQueue.Dequeue();
                waiter.inventory.Add(pair.Key, pair.Value);
            }
        }
        nodeState = NodeState.SUCCEED;
        return nodeState;
    }
}
