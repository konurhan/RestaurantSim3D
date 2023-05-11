using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RestaurantManager : MonoBehaviour
{
    public static RestaurantManager Instance;

    [SerializeField] public int money;
    [SerializeField] public int capacity;
    [SerializeField] public float popularity;

    public int satisfiedCustomers;//reset at the end of the day
    public int angryCustomers;//reset at the end of the day
    public int deniedCustomers;//reset at the end of the day

    public Transform RestaurantComponents;
    public Transform InGameUIComponents;

    public List<GameObject> seats;

    public GameObject menu;

    public List<GameObject> waiters;
    public List<GameObject> cooks;

    public Queue<GameObject> orderRequestQueue;//customers waiting to make an order
    public Queue<CustomerOrder> orderQueue;//recipes are ordered
    public Queue<KeyValuePair<GameObject, Customer>> readyQueue;//real food/drink gameObjects are created their owners are paired

    public Dictionary<string, int> ingredientList;//list of ingredient types and prices

    private void Awake()
    {
        Instance = this;
        orderRequestQueue = new Queue<GameObject>();
        orderQueue = new Queue<CustomerOrder>();
        readyQueue = new Queue<KeyValuePair<GameObject, Customer>>();
        ingredientList = new Dictionary<string, int>();
    }

    private void Start()
    {
        LoadGame();
        cooks = new List<GameObject>();
        waiters = new List<GameObject>();
        InstantiateCooks();
        InstantiateWaiters();

        CacheSeats();

        Storage.instance.printStoredGoods();

        satisfiedCustomers = 0;
        angryCustomers = 0;
        deniedCustomers = 0;
    }

    void Update()
    {
        UITopBarUpdate();
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
        GameObject waiterObject = Instantiate(Resources.Load("Prefab/Waiter")) as GameObject;
        waiterObject.GetComponent<Waiter>().SetUp(newWaiterData);
        waiters.Add(waiterObject);
    }

    public void FireWaiter(GameObject oldWaiter)
    {
        waiters.Remove(oldWaiter);
        Destroy(oldWaiter);
    }

    public void FireWaiterByStats(WaiterData firedWaiterData)
    {
        foreach (GameObject waiterObject in waiters)
        {
            if (waiterObject.GetComponent<Waiter>().CompareWithData(firedWaiterData))
            {
                waiters.Remove(waiterObject);
                Destroy(waiterObject);
                return;
            }
        }
        Debug.Log("Couldn't find the waiter corresponding to the waiterData, deletion is unsuccesfull");
    }

    public void HireCook(CookData newCookData)
    {
        GameObject cookObject = Instantiate(Resources.Load("Prefab/Cook")) as GameObject;
        cookObject.GetComponent<Cook>().SetUp(newCookData);
        cooks.Add(cookObject);
    }

    public void FireCook(GameObject oldCook)
    {
        cooks.Remove(oldCook);
        Destroy(oldCook);
    }

    public void FireCookByStats(CookData firedCookData)
    {
        foreach (GameObject cookObject in cooks)
        {
            if (cookObject.GetComponent<Cook>().CompareWithData(firedCookData))
            {
                cooks.Remove(cookObject);
                Destroy(cookObject);
                return;
            }
        }
        Debug.Log("Couldn't find the cook corresponding to the cookData, deletion is unsuccesfull");
    }

    public void BuyIngredient(KeyValuePair<string, int> newIngredient)
    {
        Storage.instance.AddIngredient(newIngredient);
    }

    public void ThrowIngredient(KeyValuePair<string, int> newIngredient)
    {
        Storage.instance.RemoveIngredient(newIngredient);
    }

    private void InstantiateCooks()
    {
        List<CookData> loadedCooks = SaveSystem.LoadCooks();
        if (loadedCooks == null) loadedCooks = SaveSystem.LoadCooksNull();
        else if(loadedCooks.Count == 0) loadedCooks = SaveSystem.LoadCooksNull();

        foreach (CookData data in loadedCooks)
        {
            GameObject cookObject = Instantiate(Resources.Load("Prefab/Cook")) as GameObject;
            cookObject.GetComponent<Cook>().SetUp(data);
            cooks.Add(cookObject);
        }
    }

    private void InstantiateWaiters()
    {
        List<WaiterData> loadedWaiters = SaveSystem.LoadWaiters();
        if (loadedWaiters == null) loadedWaiters = SaveSystem.LoadWaitersNull();
        else if(loadedWaiters.Count == 0) loadedWaiters = SaveSystem.LoadWaitersNull();

        foreach (WaiterData data in loadedWaiters)
        {
            GameObject waiterObject = Instantiate(Resources.Load("Prefab/Waiter")) as GameObject;
            waiterObject.GetComponent<Waiter>().SetUp(data);
            waiters.Add(waiterObject);
        }
    }

    private void LoadStats()
    {
        List<int> stats = SaveSystem.LoadRestaurantStats();
        if(stats == null) stats = SaveSystem.LoadRestaurantStatsNull();
        
        capacity = stats[0];
        popularity = stats[1];
        money = stats[2];
    }

    private void SaveStats()
    {
        List<int> stats = new List<int>
        {
            capacity,
            (int)popularity,
            money
        };
        SaveSystem.SaveRestaurantStats(stats);
    }

    private void LoadIngredientList()
    {
        ingredientList = SaveSystem.LoadIngredientList();
        if (ingredientList == null) { ingredientList = SaveSystem.LoadIngredientListNull(); return; } 
        else if (ingredientList.Count==0) ingredientList = SaveSystem.LoadIngredientListNull();
    }

    private void SaveIngredientList()
    {
        SaveSystem.SaveIngredientList(ingredientList);
    }

    private void SaveCooks(List<GameObject> cookObjects)//gets a list of gameObjects and saves a list of cooks
    {
        List<Cook> cooks = new List<Cook>();
        foreach (GameObject cookObject in cookObjects)
        {
            cooks.Add(cookObject.GetComponent<Cook>());
        }
        SaveSystem.SaveCooks(cooks);
    }

    private void SaveWaiters(List<GameObject> waiterObjects)//gets a list of gameObjects and saves a list of cooks
    {
        List<Waiter> waiters = new List<Waiter>();
        foreach (GameObject waiterObject in waiterObjects)
        {
            waiters.Add(waiterObject.GetComponent<Waiter>());
        }
        SaveSystem.SaveWaiters(waiters);
    }

    public void LoadGame()//call in awake or start, includes all load functions at one place
    {
        LoadIngredientList();//load existing ingerdient types and prices
        Storage.instance.Load();//loading storage
        menu.GetComponent<Menu>().Load();
        LoadStats();

    }
    public void SaveGame()//call when a button is hit contains all save functions at one place
    {
        SaveIngredientList();
        Storage.instance.Save();
        menu.GetComponent<Menu>().Save();
        SaveStats();
        SaveCooks(cooks);
        SaveWaiters(waiters);
    }

    public void CacheSeats()
    {
        Transform tables = RestaurantComponents.GetChild(1);
        for (int i = 0; i < tables.childCount; i++)
        {
            if (tables.GetChild(i).childCount == 0)
            {
                continue;
            }
            Transform ithTable = tables.GetChild(i);
            for(int j = 0; j < ithTable.childCount; j++)
            {
                seats.Add(ithTable.GetChild(j).gameObject);
            }
        }
    }

    public void AddObjectToReadyCounter(GameObject readyOrder)
    {
        Transform readyCounter = RestaurantComponents.GetChild(5);
        readyOrder.transform.parent = readyCounter;
        ArrangeReadyCounterObjects();
    }

    public void RemoveObjectFromReadyCounter(GameObject readyOrder)
    {
        //Transform readyCounter = RestaurantComponents.GetChild(5);
        readyOrder.transform.parent = null;
        ArrangeReadyCounterObjects();
    }

    public void ArrangeReadyCounterObjects()
    {
        //int readyCount = readyQueue.Count;
        int cursor = 0;
        foreach (KeyValuePair <GameObject, Customer> pair in readyQueue) 
        {
            pair.Key.transform.localPosition = Vector3.up - 2 * Vector3.left + (cursor * Vector3.right) / 4;
            cursor++;
        }
    }

    public void StartOfNewDayUpdate()
    {
        deniedCustomers = 0;
        satisfiedCustomers = 0;
        angryCustomers = 0;
    }

    public void UITopBarUpdate()
    {
        Transform topBar = InGameUIComponents.GetChild(0);
        TextMeshProUGUI popNum = topBar.GetChild(1).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI moneyNum = topBar.GetChild(3).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI servedNum = topBar.GetChild(5).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI deniedNum = topBar.GetChild(7).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI capacityNum = topBar.GetChild(9).GetComponent<TextMeshProUGUI>();

        popNum.text = popularity.ToString();
        moneyNum.text = money.ToString();
        servedNum.text = satisfiedCustomers.ToString();
        deniedNum.text = deniedCustomers.ToString();
        capacityNum.text = capacity.ToString();
    }


}
