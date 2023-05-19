using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerArrivalManager : MonoBehaviour
{
    public static CustomerArrivalManager Instance;

    public Transform customerSpawnTransform;

    public int counter;

    public int count = 0;

    private int dailyCount;

    private bool dayHasEnded;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartOfTheDayOperations();
    }

    private void FixedUpdate()
    {
        count++;
        if (counter == count && !dayHasEnded) 
        {
            InstantiateCustomer();
            count = 0;
        }
        
    }

    public void CheckForEndOfTheDay()
    {
        if (RestaurantManager.Instance.angryCustomers + RestaurantManager.Instance.satisfiedCustomers + RestaurantManager.Instance.deniedCustomers == dailyCount)
        {
            EndOfTheDayOperations();
        }
    }

    public void StartOfTheDayOperations()
    {
        dayHasEnded = false;
        float pop = RestaurantManager.Instance.popularity;
        dailyCount = (int)Random.Range(pop - pop / 10, pop + pop / 10);
        counter = (int)(180 / Time.fixedDeltaTime) / dailyCount;
    }

    public void EndOfTheDayOperations()
    {
        dayHasEnded = true;
        //finish the day

        //reset certain parammeters


        //open end of the day canvas
    }

    public void InstantiateCustomer()//instantiate customers to arrive at random times of the day
    {
        GameObject customer = Instantiate(Resources.Load("Prefab/Customer"), customerSpawnTransform.position, Quaternion.identity) as GameObject;
        customer.transform.SetParent(RestaurantManager.Instance.RestaurantComponents.GetChild(6), true);
    }

    
}
