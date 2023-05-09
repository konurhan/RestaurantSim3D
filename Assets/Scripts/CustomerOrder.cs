using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerOrder
{
    public IRecipeData data;
    public Customer orderer;
    public bool isDelivered;

    public CustomerOrder(IRecipeData data, Customer orderer)
    {
        this.data = data;
        this.orderer = orderer;
        this.isDelivered = false;
    }
}
