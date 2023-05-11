using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConsumeTask : Node
{
    Customer customer;
    public ConsumeTask(Customer customer)
    {

        this.customer = customer;

    }
    public override NodeState Evaluate()
    {
        if(customer.inventory.Count == 0)
        {
            nodeState = NodeState.FAILED;
            return nodeState;
        }
        GameObject inventoryFoodDrink = customer.inventory[0];
        IRecipe order;
        if (inventoryFoodDrink.GetComponent<Food>()) order = inventoryFoodDrink.GetComponent<Food>();//if food is delivered
        else order = inventoryFoodDrink.GetComponent<Drink>();//if drink is delivered

        customer.Hunger += order.Hunger;
        customer.Thirst += order.Thirst;

        if (customer.Hunger > 100) customer.Hunger = 100;
        if (customer.Thirst > 100) customer.Thirst = 100;

        customer.Satisfaction += order.Quality;
        customer.ServiceScore += (int)(customer.Patience - customer.basePatience/2f)/customer.basePatience;

        //after consuming
        customer.inventory.Remove(inventoryFoodDrink);
        customer.DestroyConsumedItem(inventoryFoodDrink);

        nodeState = NodeState.SUCCEED;
        return nodeState;
    }

   
}
