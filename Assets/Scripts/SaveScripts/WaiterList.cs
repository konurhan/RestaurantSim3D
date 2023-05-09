using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaiterList
{
    public List<WaiterData> waiters;//turning waiters into watirData

    public WaiterList(List<Waiter> newWaiters)
    {
        waiters = new List<WaiterData>();
        WaiterData tempdata;
        foreach(Waiter w in newWaiters)
        {
            tempdata = new WaiterData(w);
            waiters.Add(tempdata);
        }
    }

    
}
