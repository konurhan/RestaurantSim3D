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
    public int basePatience;
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
    private void Start()
    {
        agent = transform.gameObject.GetComponent<NavMeshAgent>();
        basePatience = 10000;
        inventory = new List<GameObject>();
        /*isWaitingToEatDrink = true;
        isWaitingToOrder = true;
        patience = basePatience;
        satisfactionScore = 0;
        hunger = Random.Range(75, 80);
        thirst = Random.Range(75, 80);
        serviceScore = 0;
        isSeated = false;
        seatTransform = null;//seat is not assigned
        isOrdered = false;
        isOrderArrived = false;*/
        ResetStatsAfterReturnToPool();
        gameObject.GetComponent<Animator>().speed = 2.0f;
    }
    private void Update()
    {

    }
    public void ResetStatsAfterReturnToPool()
    {
        isWaitingToEatDrink = true;
        isWaitingToOrder = true;
        patience = basePatience;
        satisfactionScore = 0;
        hunger = Random.Range(75, 80);
        thirst = Random.Range(75, 80);
        serviceScore = 0;
        inventory.Clear();
        isSeated = false;
        seatTransform = null;//seat is not assigned
        isCalledWaiter = false;
        isOrdered = false;
        isOrderArrived = false;
        isLeaving = false;
    }

    public void FindAnEmptySeat()//called after instantiation, find navigate and sit to an empty seat
    {

    }

    public void DestroyConsumedItem(GameObject inventoryFoodDrink)
    {
        ObjectPooling.Instance.SetPooledRecipe(inventoryFoodDrink);
    }

    public void TakeOnOrder(GameObject order)
    {
        inventory.Add(order);
        RestaurantManager.Instance.money += order.GetComponent<IRecipe>().Price;//customer makes the payment
        RestaurantManager.Instance.dailyEarnings += order.GetComponent<IRecipe>().Price;
    }

    public void RateService()
    {
        float score = satisfactionScore + serviceScore;
        RestaurantManager.Instance.popularity += score/400;
        RestaurantManager.Instance.dailyPopularityChange += score/400;
    }

    public void Leave()
    {
        RateService();
        ObjectPooling.Instance.SetPooledCustomer(gameObject);//poling rather than destroying
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
