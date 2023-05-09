using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restaurant : MonoBehaviour
{
    [SerializeField] public int capacity;
    [SerializeField] public int popularity;
    
    
    public GameObject menu;
    public Storage storage;
    
    public List<GameObject> waiters;
    public List<GameObject> cooks;

    private void Start()
    {
        LoadStats();

        cooks = new List<GameObject>();
        waiters = new List<GameObject>();
        
        storage = Storage.instance;//chek if it works like this or just loose the variable and use Storage.instance in the code instead
        storage.Load();
        menu.GetComponent<Menu>().Load();
        
        InstantiateCooks();
        InstantiateWaiters();
        
    }

    public void IncreaseCapacity()
    {
        capacity++;
    }

    public void DecreaseCapacity()
    {
        capacity--;
    }

    public void UpdatePopularity(int newPopularity)
    {
        popularity = newPopularity;
    }

    public void HireWaiter(WaiterData newWaiterData)
    {
        GameObject waiterObject = Instantiate(Resources.Load("Workers/templateWaiter")) as GameObject;
        waiterObject.GetComponent<Waiter>().SetUp(newWaiterData);
        waiters.Add(waiterObject);
    }

    public void FireWaiter(GameObject oldWaiter)
    {
        waiters.Remove(oldWaiter);
    }

    public void HireCook(CookData newCookData)
    {
        GameObject cookObject = Instantiate(Resources.Load("Workers/templateCook")) as GameObject;
        cookObject.GetComponent<Cook>().SetUp(newCookData);
        cooks.Add(cookObject);
    }

    public void FireCook(GameObject oldCook)
    {
        cooks.Remove(oldCook);
    }

    public void BuyIngredient(KeyValuePair<string, int> newIngredient)
    {
        storage.AddIngredient(newIngredient);
    }

    public void ThrowIngredient(KeyValuePair<string, int> newIngredient)
    {
        storage.RemoveIngredient(newIngredient);
    }

    private void InstantiateCooks()
    {
        List<CookData> loadedCooks = SaveSystem.LoadCooks();
        foreach (CookData data in loadedCooks)
        {
            GameObject cookObject = Instantiate(Resources.Load("Workers/templateCook")) as GameObject;
            cookObject.GetComponent<Cook>().SetUp(data);
            cooks.Add(cookObject);
        }
    }

    private void InstantiateWaiters()
    {
        List<WaiterData> loadedWaiters = SaveSystem.LoadWaiters();
        foreach (WaiterData data in loadedWaiters)
        {
            GameObject waiterObject = Instantiate(Resources.Load("Workers/templateWaiter")) as GameObject;
            waiterObject.GetComponent<Waiter>().SetUp(data);
            waiters.Add(waiterObject);
        }
    }

    private void LoadStats()
    {
        List<int> stats = SaveSystem.LoadRestaurantStats();
        capacity = stats[0];
        popularity= stats[1];
    }

    public void SaveGame()
    {

    }
    
}
