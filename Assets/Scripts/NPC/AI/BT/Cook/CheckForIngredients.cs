using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForIngredients : Node
{
    Cook cook;
    Animator animator;
    public CheckForIngredients(Cook cook)
    {
        this.cook = cook;
        animator = cook.gameObject.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        if (cook.cooking)
        {
            nodeState = NodeState.SUCCEED;
            return nodeState;
        }
        else
        {
            if (cook.currentOrder != null)
            {
                Debug.Log("currentOreder is not null");
            }
            if (cook.IsEnoughInventory(cook.currentOrder.data))
            {
                //animator.SetBool("isWalking", false);
                animator.SetBool("isCooking", true);
                cook.cooking = true;
                cook.preperationTime = (int)(1000 / cook.speed);
                nodeState = NodeState.SUCCEED;
                return nodeState;
            }
            else
            {
                Debug.Log("Inventory is not enough for recipe");
                RestaurantManager.Instance.orderQueue.Enqueue(cook.currentOrder);//test if this works
                cook.currentOrder = null;
                nodeState = NodeState.FAILED;
                return nodeState;
            }
        }
    }
}
