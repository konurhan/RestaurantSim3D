using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CookList
{
    public List<CookData> cooks;

    public CookList(List<Cook> newCooks) //turning cooks into cookData
    {
        cooks = new List<CookData>();
        CookData tempdata;
        foreach (Cook c in newCooks)
        {
            tempdata = new CookData(c);
            cooks.Add(tempdata);
        }
    }
}
