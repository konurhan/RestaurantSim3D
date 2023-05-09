using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour, IRecipe
{
    [SerializeField] private string recipeName;
    [SerializeField] private int quality;
    [SerializeField] private Dictionary<KeyValuePair<string, int>, int> ingredients;
    [SerializeField] private int price;
    [SerializeField] private int thirst;
    [SerializeField] private int hunger;
    [SerializeField] private Customer orderer;

    public string Name
    {
        get { return recipeName; }
        set { recipeName = value; }
    }
    public Dictionary<KeyValuePair<string, int>, int> Ingredients
    {
        get { return ingredients; }
        set { ingredients = value; }
    }
    public int Price
    {
        get { return price; }
        set { price = value; }
    }
    public int Thirst
    {
        get { return thirst; }
        set { thirst = value; }
    }
    public int Hunger
    {
        get { return hunger; }
        set { hunger = value; }
    }
    public int Quality
    {
        get { return quality; }
        set { quality = value; }
    }
    public Customer Orderer
    {
        get { return orderer; }
        set { orderer = value; }
    }
    public void SetPrice(int newPrice)
    {
        price = newPrice;
    }
    public void SetValues(IRecipeData data)
    {
        recipeName = data.Name;
        ingredients = data.Ingredients;
        price = data.Price;
        thirst = data.Thirst;
        hunger = data.Hunger;
    }
}
