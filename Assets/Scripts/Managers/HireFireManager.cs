using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HireFireManager : MonoBehaviour
{
    
    

    public void HireGenericWaiter()
    {
        int speed = (int)Random.Range(1, 3);
        int capacity = (int)Random.Range(1, 3);
        int wage = (int)Random.Range(1, 5);
        WaiterData tempWaiterData = new WaiterData(0, 1, speed, capacity, wage);
        RestaurantManager.Instance.HireWaiter(tempWaiterData);
        MenuController.Instance.SetupWaitersPopup();
    }

    public void HireGenericCook()
    {
        int speed = (int)Random.Range(1, 3);
        int talent = (int)Random.Range(1, 3);
        int wage = (int)Random.Range(1, 5);
        CookData tempCookData = new CookData(0, 1, speed, talent, wage);
        RestaurantManager.Instance.HireCook(tempCookData);
        MenuController.Instance.SetupCooksPopup();
    }

    public void FireWaiterByIndex(int index)
    {
        RestaurantManager.Instance.FireWaiterByIndex(index);
        MenuController.Instance.SetupWaitersPopup();
    }

    public void FireCookByIndex(int index)
    {
        RestaurantManager.Instance.FireCookByIndex(index);
        MenuController.Instance.SetupCooksPopup();
    }
}
