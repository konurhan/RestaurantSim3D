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
    Animator animator;
    public RecieveOrdersTask(Waiter waiter)
    {
        this.waiter = waiter;
        agent = waiter.GetComponent<NavMeshAgent>();
        animator = waiter.gameObject.GetComponent<Animator>();
    }

    public override NodeState Evaluate()//this method doesn't work properly, if once succeded it always return succeed
    {
        if(!waiter.pickedUpOrders)
        {
            waiter.pickedUpOrders = true;
            waiter.PickUpReadyOrders();

            foreach (GameObject ord in waiter.inventory.Keys.ToList())
            {
                if (waiter.inventory[ord] == null)
                {
                    Debug.Log("waiter has pickud-up an order for a customer who has already left");
                    waiter.inventory.Remove(ord);
                    ord.GetComponent<IRecipe>().DestroyObject();
                }
            }

            if (waiter.inventory.Count == 0)
            {
                Debug.Log(waiter.gameObject.name + " has arrived to pick up ready orders but all orders are already picked up.");
                waiter.pickedUpOrders = false;
                waiter.delivering = false;
                nodeState = NodeState.FAILED;
                return nodeState;
            }

            KeyValuePair<GameObject, Customer> order = waiter.inventory.First();
            agent.SetDestination(order.Value.seatTransform.position);//setting the destination for the first customer
            animator.SetBool("isWalking", true);
        }
        nodeState = NodeState.SUCCEED;
        return nodeState;
    }
}
