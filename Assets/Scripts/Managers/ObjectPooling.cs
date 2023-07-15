using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance;
    //public List<GameObject> pooledWaiters;
    //public List<GameObject> pooledCooks;
    public Dictionary<string, List<GameObject>> pooledRecipes;
    public int recipePoolingAmount;
    public List<GameObject> pooledCustomers;
    public int customerPoolingAmount;
    private Transform customerSpawnTransform;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        customerSpawnTransform = CustomerArrivalManager.Instance.customerSpawnTransform;
        pooledRecipes = new Dictionary<string, List<GameObject>>();
        PoolCustomers();
        Invoke("PoolMenuElements", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PoolMenuElements()
    {
        List<FoodData> foodDatas = RestaurantManager.Instance.menu.GetComponent<Menu>().Foods;
        List<DrinkData> drinkDatas = RestaurantManager.Instance.menu.GetComponent<Menu>().Drinks;
        
        foreach(FoodData data in foodDatas)
        {
            for (int i = 0; i < recipePoolingAmount; i++)
            {
                InstantiateAndAddRecipe(true, data.Name);
            }
        }
        foreach (DrinkData data in drinkDatas)
        {
            for (int i = 0; i < recipePoolingAmount; i++)
            {
                InstantiateAndAddRecipe(false, data.Name);
            }
        }
    }

    public void InstantiateAndAddRecipe(bool isFood, string name)
    {
        GameObject recipe;
        if (isFood)
        {
            Debug.Log("instantiating food");
            recipe = Instantiate(Resources.Load("Menu/Foods/" + name)) as GameObject;
        }
        else
        {
            recipe = Instantiate(Resources.Load("Menu/Drinks/" + name)) as GameObject;
        }
        recipe.SetActive(false);
        if (pooledRecipes.ContainsKey(name))
        {
            pooledRecipes[name].Add(recipe);
        }
        else
        {
            pooledRecipes.Add(name, new List<GameObject> { recipe } );
        }
        recipe.transform.SetParent(RestaurantManager.Instance.RestaurantComponents.GetChild(7), true);
    }

    public GameObject GetPooledRecipe(bool isFood, string recipeName)
    {
        GameObject recipe = null;
        for (int i = 0; i< pooledRecipes[recipeName].Count; i++)
        {
            if (!pooledRecipes[recipeName][i].activeInHierarchy)
            {
                recipe = pooledRecipes[recipeName][i];
                break;
            }
        }
        if (recipe == null)
        {
            InstantiateAndAddRecipe(isFood, recipeName);
            recipe = pooledRecipes[recipeName][pooledRecipes[recipeName].Count - 1];
        }

        return recipe;
    }

    public void SetPooledRecipe(GameObject usedRecipe)
    {
        usedRecipe.GetComponent<IRecipe>().ResetStats();
        usedRecipe.SetActive(false);
        usedRecipe.transform.SetParent(RestaurantManager.Instance.RestaurantComponents.GetChild(7), true);
    }
    public void PoolCustomers()
    {
        for(int i = 0; i < customerPoolingAmount; i++)
        {
            InstantiateAndAddCustomer();
        }
    }

    public void InstantiateAndAddCustomer()
    {
        GameObject customer = Instantiate(Resources.Load("Prefab/Customer"), customerSpawnTransform.position, Quaternion.identity) as GameObject;
        customer.transform.SetParent(RestaurantManager.Instance.RestaurantComponents.GetChild(6), true);
        customer.SetActive(false);
        pooledCustomers.Add(customer);
    }

    public GameObject GetPooledCustomer()
    {
        //Debug.Log("GetPooledCustomer is called");
        GameObject customer = null;
        for (int i = 0;i < pooledCustomers.Count;i++)
        {
            if (!pooledCustomers[i].activeInHierarchy)
            {
                customer = pooledCustomers[i];
                //Debug.Log("found a suitable customer");
                break;
            }   
        }
        if (customer == null)
        {
            //Debug.Log("going to add a new customer to the pool");
            InstantiateAndAddCustomer();
            customerPoolingAmount++;
            customer = pooledCustomers[pooledCustomers.Count - 1];
        }
        else
        {
            //Debug.Log("customer is not null");
            //Debug.Log(customer.name);
        }


        if (customer.activeInHierarchy)
        {
            //Debug.Log("GetPooledCustomer returns an already active object, probably couldn't locate newly added customer in the pooledCustomers list");
        }
        customer.transform.position += Vector3.up * 2;
        return customer;
    }

    public void SetPooledCustomer(GameObject usedObject)
    {
        //there might be other required operations than just deactivating the object
        usedObject.SetActive(false);
        usedObject.GetComponent<Customer>().ResetStatsAfterReturnToPool();
        usedObject.transform.position = customerSpawnTransform.position;
    }
}
