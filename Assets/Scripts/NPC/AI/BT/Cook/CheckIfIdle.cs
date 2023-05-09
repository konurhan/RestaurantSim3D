using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckIfIdle : Node
{
    Cook cook;
    public CheckIfIdle(Cook cook)
    {
        this.cook = cook;
    }
    public override NodeState Evaluate()
    {
        if(!cook.delivering && !cook.cooking)
        {
            if (cook.currentOrder == null)
            {
                if (RestaurantManager.Instance.orderQueue.Count != 0)//cook taken-on new order
                {
                    Debug.Log("cook taken-on new order");
                    cook.currentOrder = RestaurantManager.Instance.orderQueue.Dequeue();
                    Transform kitchenCounter = RestaurantManager.Instance.RestaurantComponents.GetChild(3).gameObject.transform;
                    cook.gameObject.GetComponent<NavMeshAgent>().SetDestination(kitchenCounter.position);
                    nodeState = NodeState.SUCCEED;
                    return nodeState;
                }
                else//no orders to prepare
                {
                    Debug.Log("no orders to prepare");
                    nodeState = NodeState.FAILED;
                    return nodeState;
                }
            }
            else//cook already has taken-on an order
            {
                nodeState = NodeState.SUCCEED;
                return nodeState;
            }   
        }
        else//cook is already cooking or delivering
        {
            nodeState = NodeState.FAILED;
            return nodeState;
        }
    }
}
