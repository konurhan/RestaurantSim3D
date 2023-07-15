using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeAnOrderTask : Node
{
    Customer customer;
    public MakeAnOrderTask(Customer customer)
    {
        this.customer = customer;
    }
    public override NodeState Evaluate()
    {
        //Menu menu = RestaurantManager.Instance.menu.GetComponent<Menu>();
        //make a random order here
        IRecipeData order; 
        if(customer.Thirst <= customer.Hunger)
        {
            int randFood = Random.Range(0, RestaurantManager.Instance.menu.GetComponent<Menu>().Foods.Count);
            Debug.Log("customer is ordering randFood: " + randFood);
            Debug.Log("menu food recipe count is "+ RestaurantManager.Instance.menu.GetComponent<Menu>().Foods.Count);
            order = RestaurantManager.Instance.menu.GetComponent<Menu>().Foods[randFood];
            
        }
        else
        {
            int randDrink = Random.Range(0, RestaurantManager.Instance.menu.GetComponent<Menu>().Drinks.Count);
            Debug.Log("customer is ordering randDrink: " + randDrink);
            Debug.Log("menu drink recipe count is " + RestaurantManager.Instance.menu.GetComponent<Menu>().Drinks.Count);
            order = RestaurantManager.Instance.menu.GetComponent<Menu>().Drinks[randDrink];
        }

        Debug.Log("Customer order is a " +  order.Name);

        Customer orderer = customer;
        CustomerOrder newOrder = new CustomerOrder(order, orderer);
        RestaurantManager.Instance.orderQueue.Enqueue(newOrder);

        customer.isOrdered = true;
        nodeState = NodeState.SUCCEED;
        return nodeState;
    }
}
