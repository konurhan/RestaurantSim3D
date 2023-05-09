using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    [SerializeField] private int patience;//on the scale of 100
    [SerializeField] private int satisfactionScore;//on the scale of 100
    [SerializeField] private int hunger;//on the scale of 100
    [SerializeField] private int thirst;//on the scale of 100
    [SerializeField] private int serviceScore;
    public bool isSeated;
    public bool isCalledWaiter;
    public bool isOrdered;
    public bool isOrderArrived;
    public Transform seatTransform;
    public bool isWaitingToEatDrink;
    public bool isWaitingToOrder;
    public bool isLeaving;
    public List<GameObject> inventory;

    public NavMeshAgent agent;

    /*public bool IsWaiting 
    {
        get { return isWaiting; }
        set { isWaiting = value; }
    }*/

    private void Start()
    {
        agent = transform.gameObject.GetComponent<NavMeshAgent>();
        isWaitingToEatDrink = true;
        isWaitingToOrder = true;
        patience = 100000;
        satisfactionScore = 0;
        hunger = Random.Range(0, 60);
        thirst = Random.Range(0, 60);
        serviceScore = 0;
        inventory = new List<GameObject>();
        isSeated = false;
        seatTransform = null;//seat is not assigned
        isOrdered = false;
    }

    private void Update()
    {

    }

    public void FindAnEmptySeat()//called after instantiation, find navigate and sit to an empty seat
    {

    }

    public void DestroyConsumedItem(GameObject inventoryFoodDrink)
    {
        Destroy(inventoryFoodDrink);
    }
    public void Consume(GameObject inventoryFoodDrink)//make this a coroutine
    {
        IRecipe order;
        if (inventoryFoodDrink.GetComponent<Food>()) order = inventoryFoodDrink.GetComponent<Food>();//if food is delivered
        else order = inventoryFoodDrink.GetComponent<Drink>();//if drink is delivered

        hunger += order.Hunger;
        thirst += order.Thirst;

        if (hunger > 100) hunger = 100;
        if (thirst > 100) thirst = 100;

        satisfactionScore = order.Quality;
        serviceScore = patience;

        //after consuming
        inventory.Remove(inventoryFoodDrink);
        Destroy(inventoryFoodDrink);
    }

    /*public void CallWaiterToOrder(IRecipeData orderData)
    {
        isWaitingToOrder = true;
        isWaitingToEatDrink = true;
        RestaurantManager.Instance.orderRequestQueue.Enqueue(this.gameObject);//adding customer to the order request queue soon a waiter will arrive.
        StartCoroutine(WaitForOrder());
    }*/

    /*public void GiveOrder()//this will be ticked continuously by the behavior tree
    {
        //isWaiting = false;
        if (!isWaitingToOrder)
        {
            //make a random order here
            IRecipeData order = RestaurantManager.Instance.menu.GetComponent<Menu>().Foods[0];
            //
            Customer orderer = this;
            CustomerOrder newOrder = new CustomerOrder(order, orderer);
            RestaurantManager.Instance.orderQueue.Enqueue(newOrder);
        }
    }*/

    public void TakeOnOrder(GameObject order)
    {
        inventory.Add(order);
    }
    /*public IEnumerator WaitForOrder()
    {
        while (true)
        {
            if (patience > 0) patience--;
            else { Leave(); break; }

            if (!isWaitingToEatDrink) break;
            yield return new WaitForSeconds(5);
        }
    }*/
    public void RateService()
    {
        float score = ((satisfactionScore + serviceScore) / 100) - 1;//score is bw -1 and 1
        RestaurantManager.Instance.popularity += score;
    }

    public void Leave()
    {
        RateService();
    }

    public int Thirst
    {
        get { return thirst; } 
        set {  thirst = value; }
    }

    public int Hunger
    {
        get { return hunger; }
        set { hunger = value; }
    }

    public int Patience
    {
        get { return patience; }
        set { patience = value; }
    }

    public int Satisfaction
    {
        get { return satisfactionScore; }
        set { satisfactionScore = value; }
    }

    public int ServiceScore
    {
        get { return serviceScore; }
        set { serviceScore = value; }
    }
}
