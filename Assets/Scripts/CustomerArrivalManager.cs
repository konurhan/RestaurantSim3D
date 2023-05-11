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

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InstantiateCustomersOfRandomQuantity();
        counter = 2000;
        
    }

    private void Update()
    {
        count++;
        if (counter == count) 
        {
            InstantiateCustomersOfRandomQuantity();
            count = 0;
        }

        //put this inside a method and trigger it after all the customers arrived
        //or better make this check inside RestaurantManager and then trigger EndOfTheDayOperations method
        if(RestaurantManager.Instance.angryCustomers+ RestaurantManager.Instance.satisfiedCustomers+ RestaurantManager.Instance.deniedCustomers == dailyCount) 
        { 
            //finish the day
        }
    }

    public void StartOfTheDayOperations()
    {
        float pop = RestaurantManager.Instance.popularity;
        dailyCount = (int)Random.Range(pop - pop / 10, pop + pop / 10);
    }

    public void InstantiateCustomersOfRandomQuantity()//instantiate customers to arrive at random times of the day
    {
        GameObject customer = Instantiate(Resources.Load("Prefab/Customer"), customerSpawnTransform.position, Quaternion.identity) as GameObject;
    }


}
