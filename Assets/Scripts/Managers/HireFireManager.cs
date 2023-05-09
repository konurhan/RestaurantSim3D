using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HireFireManager : MonoBehaviour
{
    WaiterData tempWaiterData = new WaiterData(5, 10, 10, 10, 10);
    CookData tempCookData = new CookData(5, 10, 10, 10, 10);

    public void HireGenericWaiter()
    {
        RestaurantManager.Instance.HireWaiter(tempWaiterData);
    }

    public void HireGenericCook()
    {
        RestaurantManager.Instance.HireCook(tempCookData);
    }

    public void FireGenericWaiter()
    {
        RestaurantManager.Instance.FireWaiterByStats(tempWaiterData);
    }

    public void FireGenericCook()
    {
        RestaurantManager.Instance.FireCookByStats(tempCookData);
    }
}
