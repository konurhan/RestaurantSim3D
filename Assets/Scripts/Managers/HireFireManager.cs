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
        MenuController.Instance.SetupWaitersPopup();
    }

    public void HireGenericCook()
    {
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
