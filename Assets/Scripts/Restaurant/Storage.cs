using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] private Dictionary<KeyValuePair<string,int>, int> storedGoods;
    public static Storage instance;

    private void Awake()
    {
        instance = this;
        storedGoods = new Dictionary<KeyValuePair<string, int>, int>();

    }

    public Dictionary<KeyValuePair<string, int>, int> StoredGoods
    { 
        get { return storedGoods; }
        set { storedGoods = value; }
    }

    public void AddIngredient(KeyValuePair<string, int> ingredient)
    {
        if (storedGoods.ContainsKey(ingredient)) storedGoods[ingredient]++;
        else { storedGoods.Add(ingredient, 1); }
    }
    
    public void RemoveIngredient(KeyValuePair<string, int> ingredient)
    {
        if (storedGoods.ContainsKey(ingredient)) storedGoods[ingredient]--;
        else { return; }
    }

    public bool IfEnoughThenAllocate(Dictionary<KeyValuePair<string, int>, int> recipeContents)
    {
        foreach ((KeyValuePair<string, int> good, int amount) in recipeContents)
        {
            if (!storedGoods.ContainsKey(good) || amount > storedGoods[good]) // do the comparison on names rather than objects, objects are not same, they are different instances of the same class
            {
                Debug.Log("Not enough " + good.Key + " , needed: "+amount+", stored: "+ storedGoods[good]);
                return false;
            } 
        }
        foreach ((KeyValuePair<string, int> good, int amount) in recipeContents)
        {
            storedGoods[good] -= amount;
        }
        Debug.Log("There are enough ingredients, cook will start to prepare the order");
        return true;
    }

    public void Load()
    {
        storedGoods = SaveSystem.LoadStorage();
        if (storedGoods == null) storedGoods = SaveSystem.LoadStorageNull();
        else if (storedGoods.Count == 0) storedGoods = SaveSystem.LoadStorageNull();
    }

    public void Save()
    {
        SaveSystem.SaveStorage(storedGoods);
    }

    public void printStoredGoods()
    {
        foreach (KeyValuePair<KeyValuePair<string, int>, int> good in storedGoods)
        {
            Debug.Log("type: " + good.Key.Key + ", quantity: " + good.Value);
        }
    }
}
