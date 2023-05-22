using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CarryOrdersToCustomersTask : Node
{
    private Waiter waiter;
    private NavMeshAgent agent;
    Animator animator;
    public CarryOrdersToCustomersTask(Waiter waiter)
    {
        this.waiter = waiter;
        agent = waiter.GetComponent<NavMeshAgent>();
        animator = waiter.gameObject.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    animator.SetBool("isWalking", false);
                    Debug.Log("waiter arrived to a customer");
                    KeyValuePair<GameObject, Customer> prevOrder = waiter.inventory.First();//hata veriyor, inventory boş
                    waiter.DeliverOrder(prevOrder.Key, prevOrder.Value);
                    waiter.inventory.Remove(prevOrder.Key);//removing the customer just served
                    
                    while (waiter.inventory.Count > 0)
                    {
                        KeyValuePair<GameObject, Customer> order = waiter.inventory.First();
                        if (order.Value != null)
                        {
                            animator.SetBool("isWalking", true);
                            agent.SetDestination(order.Value.seatTransform.position);
                            break;
                        }
                        else
                        {
                            waiter.inventory.Remove(order.Key);
                            order.Key.GetComponent<IRecipe>().DestroyObject();
                        }
                    }
                    
                    if(waiter.inventory.Count > 0)// a new destination is set
                    {
                        nodeState = NodeState.RUNNING;
                        return nodeState;
                    }
                    else//delivered all orders in the inventory, inventory is empty, no new destinations
                    {
                        animator.SetBool("isWalking", false);
                        waiter.pickedUpOrders = false;
                        waiter.delivering = false;
                        nodeState = NodeState.SUCCEED;
                        return nodeState;
                    }
                    /*if (waiter.inventory.Count > 0)
                    {
                        KeyValuePair<GameObject,Customer> order = waiter.inventory.First();
                        if(order.Value != null)
                        {
                            agent.SetDestination(order.Value.seatTransform.position);
                        }
                        else
                        {
                            waiter.inventory.Remove(order.Key);
                            order.Key.GetComponent<IRecipe>().DestroyObject();
                        }
                        
                        nodeState = NodeState.RUNNING;
                        return nodeState;
                    }
                    else//delivered all orders in the inventory
                    {
                        waiter.pickedUpOrders = false;
                        waiter.delivering = false;
                        nodeState = NodeState.SUCCEED;
                        return nodeState;
                    }*/
                }
            }
        }
        nodeState = NodeState.RUNNING;
        return nodeState;
    }
}
