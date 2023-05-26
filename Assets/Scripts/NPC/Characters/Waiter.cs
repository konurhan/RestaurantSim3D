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
    public bool pickedUpOrders;

    public NavMeshAgent agent;

    private void Start()
    {
        agent = transform.gameObject.GetComponent<NavMeshAgent>();
        inventory = new Dictionary<GameObject, Customer>();
        pickedUpOrders = false;
    }
    public bool CanLevelUp()
    {
        if (xp >= (level * 10) + 10) { return true; }
        return false;
    }
    public void LevelUp()
    {
        if (CanLevelUp()) 
        { 
            level++; speed++; capacity++; wage++;
            Debug.Log("a waiter has leveled up");
            
            NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
            agent.speed++;
            agent.acceleration += 5;
            agent.angularSpeed += 10;
        }
        else { Debug.Log("waiter do not have enough xp's to level up"); }
    }

    public void PickUpReadyOrders()//if idle and readyque is not empty
    {
        while(inventory.Count < capacity) 
        {
            if (RestaurantManager.Instance.readyQueue.Count == 0)
            {
                break;
            }
            KeyValuePair<GameObject, Customer> pair = RestaurantManager.Instance.readyQueue.Dequeue();
            RestaurantManager.Instance.RemoveObjectFromReadyCounter(pair.Key);
            inventory.Add(pair.Key, pair.Value);
            pair.Key.transform.parent = gameObject.transform;
        }
        ArrangeOnTrayObjects();

    }

    public void PutOrderOnTray(GameObject order)
    {
        inventory.Add(order, order.GetComponent<IRecipe>().Orderer);
        ArrangeOnTrayObjects();
    }

    public void RemoveOrderFromTray(GameObject order)
    {
        inventory.Remove(order);
        ArrangeOnTrayObjects();
    }

    public void ArrangeOnTrayObjects()
    {
        Transform centerOfTray = gameObject.transform.GetChild(0);
        //Debug.Log("center of tray local position is" + centerOfTray.localPosition);
        int onTrayOrdersCount = inventory.Count;
        int sliceCount = 0;
        float radius = 0.5f;
        foreach (KeyValuePair<GameObject, Customer> order in inventory)
        {
            float degree = 2*Mathf.PI * sliceCount / onTrayOrdersCount;
            //Debug.Log("degree of a slice is " + degree);
            Vector3 offset = new Vector3(radius*Mathf.Sin(degree), 0, -radius * Mathf.Cos(degree));
            //Debug.Log("offset of slice " + (sliceCount + 1) + " is: " + offset);
            order.Key.transform.localPosition = centerOfTray.localPosition + offset;

            //Debug.Log("localPos of slice "+(sliceCount+1)+" is: " + order.Key.transform.localPosition);
            sliceCount++;
        }
    }

    public void DeliverOrder(GameObject order, Customer customer)//hand in the order to the customer
    {
        //carry the order to the customer and leave on the table
        if(customer.isWaitingToEatDrink && !customer.isLeaving)//do this case where the DeliverOrder function is triggered.
        {
            RemoveOrderFromTray(order);
            customer.TakeOnOrder(order);
            customer.isWaitingToEatDrink = false;
            customer.isOrderArrived = true;
            GainExperience();
            //LevelUp();
        }
        
        else if(customer.isLeaving)
        {
            Debug.Log("Customer is leaving, order was late. It will go to trash :(");
            inventory.Remove(order);
            Destroy(order);
        }
        
    }

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

        NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = speed + 3;
        agent.acceleration = 6 + 5 * (speed - 1);
        agent.angularSpeed = 180 + 10 * (speed - 1);
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
