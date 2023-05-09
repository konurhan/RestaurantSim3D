using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

//If available decide what to do
//Can take orders if the orderRequestQueue is not empty
//Can deliver orders if the readyQueue is not empty

//Put if statements into behavior tree structure
public class Waiter : MonoBehaviour
{
    [SerializeField] public int xp;
    [SerializeField] public int level;
    [SerializeField] public float speed;
    [SerializeField] public int capacity;
    [SerializeField] public int wage;
    [SerializeField] public Dictionary<GameObject, Customer> inventory;//order owner pair
    [SerializeField] public Customer currentCustomer;//customer to take order from
    [SerializeField] public bool delivering;//flag that shows if a waiter is delivering or not

    public NavMeshAgent agent;

    private void Start()
    {
        agent = transform.gameObject.GetComponent<NavMeshAgent>();
    }
    public bool CanLevelUp()
    {
        if (xp >= (level * 10) + 10) { return true; }
        return false;
    }
    public void LevelUp()
    {
        if (CanLevelUp()) { level++; speed++; capacity++; wage++; }
    }
    /*public void TakeOrder(IRecipeData order, Customer orderer) 
    {
        CustomerOrder newOrder = new CustomerOrder(order, orderer);
        RestaurantManager.Instance.orderQueue.Enqueue(newOrder);
    }*/

    public void GoToPickupReadyOrders()//if idle and readyque is not empty
    {

    }
    /*public void PickUpReadyOrders()//if idle and readyque is not empty
    {
        while(inventory.Count <capacity) 
        {
            KeyValuePair<GameObject, Customer> pair = RestaurantManager.Instance.readyQueue.Dequeue();
            inventory.Add(pair.Key, pair.Value);
        }
        
    }*/
    /*public void DeliverOrders()//when inventory is full or all items are taken, deliver them one by one
    {
        foreach (KeyValuePair<GameObject,Customer> pair in inventory)
        {
            GameObject order = pair.Key;
            Customer orderer = pair.Value;
            GoToDeliverOrder(new Vector2(orderer.transform.position.x, orderer.transform.position.y));
            DeliverOrder(order, orderer);
        }
    }*/
    public void GoToDeliverOrder(Vector2 customerLocation)//go to a customers loacation
    {
        //navigate to customer
        //A* needs to be implemented to navigate the waiter NPC
    }
    public void DeliverOrder(GameObject order, Customer customer)//hand in the order to the customer
    {
        //carry the order to the customer and leave on the table
        if(customer.isWaitingToEatDrink && !customer.isLeaving)//do this case where the DeliverOrder function is triggered.
        {
            inventory.Remove(order);
            customer.TakeOnOrder(order);
            customer.isWaitingToEatDrink = false;
            customer.isOrderArrived = true;
            GainExperience();
            LevelUp();
        }
        
    }

    /*public void GoToTakeOrder()//navigate to the customer, A* code comes here ore used here
    {
        //navigate to customer
        //A* needs to be implemented to navigate the waiter NPC
    }*/

    public void GainExperience()
    {
        xp++;
    }

    public void SetUp(WaiterData data)
    {
        xp = data.xp;
        level = data.level;
        speed = data.speed;
        capacity = data.capacity;
        wage = data.wage;
    }

    public bool CompareWithData(WaiterData data)
    {
        if (data.xp != xp) return false;
        if (data.level != level) return false;
        if (data.speed != speed) return false;
        if (data.capacity!= capacity) return false;
        if (data.wage!= wage) return false;
        return true;
    }
}
