using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerArrivalManager : MonoBehaviour
{
    public static CustomerArrivalManager Instance;

    public Transform customerSpawnTransform;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InstantiateCustomersOfRandomQuantity();
    }


    public void InstantiateCustomersOfRandomQuantity()//instantiate customers to arrive at random times of the day
    {
        GameObject customer = Instantiate(Resources.Load("Prefab/Customer"), customerSpawnTransform.position, Quaternion.identity) as GameObject;
    }
}
