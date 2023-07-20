using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class CustomerArrivalManager : MonoBehaviour
{
    public static CustomerArrivalManager Instance;

    public Transform customerSpawnTransform;

    public int counter;

    public int count = 0;

    [SerializeField] private int arrived;

    [SerializeField] private int dailyCount;

    private bool dayHasEnded;

    private void Awake()
    {
        Instance = this;
        counter = 0;

    }

    private void Start()
    {
        StartOfTheDayOperations();
    }

    private void FixedUpdate()
    {
        count++;
        if (counter <= count && arrived != dailyCount) 
        {
            InstantiateCustomer();
            count = 0;
            arrived++;
        }
        
    }

    public void CheckForEndOfTheDay()
    {
        Debug.Log("CheckForEndOfTheDay method is called");
        if (RestaurantManager.Instance.angryCustomers + RestaurantManager.Instance.satisfiedCustomers + RestaurantManager.Instance.deniedCustomers >= dailyCount)
        {
            Debug.Log("EndOfTheDayOperations method is called");
            Debug.Log(RestaurantManager.Instance.deniedCustomers + " customers are denied, "+ RestaurantManager.Instance.satisfiedCustomers + " customers are satisfied, "+ RestaurantManager.Instance.angryCustomers + " customers are angry.");
            EndOfTheDayOperations();
        }
    }

    public void StartOfTheDayOperations()
    {
        arrived = 0;
        MenuController.Instance.BottomBar.GetChild(4).gameObject.SetActive(false);
        MenuController.Instance.EndOfTheDayPopup.gameObject.SetActive(false);
        //RestaurantManager.Instance.DailyReset();
        dayHasEnded = false;
        float pop = RestaurantManager.Instance.popularity;
        dailyCount = (int)Random.Range(pop - pop / 10, pop + pop / 10);
        Debug.Log(dailyCount + " customers will visit today.");
        Debug.Log("fixed time unit: " + Time.fixedDeltaTime);
        counter = (int)(10f / Time.fixedDeltaTime) / dailyCount;
        count = 0;
    }

    public void EndOfTheDayOperations()
    {
        //finish the day
        dayHasEnded = true;
        Transform customersParent = RestaurantManager.Instance.RestaurantComponents.GetChild(6);
        foreach (Transform child in customersParent)//setting all existing customers back to the pool
        {
            Debug.Log("EndOfTheDayOperations setting a customer back to the pool");
            ObjectPooling.Instance.SetPooledCustomer(child.gameObject);
        }
        foreach (GameObject seat in RestaurantManager.Instance.seats)
        {
            seat.GetComponent<SeatController>().LeaveTheSeat();
        }
        //pay wages
        RestaurantManager.Instance.PayWages();

        //open and set values of end of the day canvas
        Transform Eof = MenuController.Instance.EndOfTheDayPopup;
        MenuController.Instance.BottomBar.GetChild(4).gameObject.SetActive(true);
        Eof.gameObject.SetActive(true);
        Eof.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = RestaurantManager.Instance.dailySpendings.ToString();
        Eof.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = RestaurantManager.Instance.dailyEarnings.ToString();
        Eof.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = RestaurantManager.Instance.satisfiedCustomers.ToString();
        Eof.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = RestaurantManager.Instance.dailyPopularityChange.ToString();
        RestaurantManager.Instance.DailyReset();
    }

    public void InstantiateCustomer()//instantiate customers to arrive at random times of the day
    {
        GameObject customer = ObjectPooling.Instance.GetPooledCustomer();
        customer.SetActive(true);
    }

    
}
