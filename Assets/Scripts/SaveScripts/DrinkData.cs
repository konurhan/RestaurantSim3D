using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DrinkData: IRecipeData
{
    public string recipeName;
    public int quality;
    public Dictionary<KeyValuePair<string, int>, int> ingredients;
    public int price;
    public int thirst;
    public int hunger;
    public bool isFood = false;

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
    public bool IsFood
    {
        get { return isFood; }
        set { isFood = value; }
    }

    public DrinkData(Drink drink)
    {
        recipeName = drink.Name;
        quality = drink.Quality;
        ingredients = drink.Ingredients;
        price = drink.Price;
        thirst = drink.Thirst;
        hunger = drink.Hunger;
    }

    public DrinkData(string recipeName, int quality, Dictionary<KeyValuePair<string, int>, int> ingredients, int price, int thirst, int hunger, bool isFood)
    {
        this.recipeName = recipeName;
        Quality = quality;
        Ingredients = ingredients;
        Price = price;
        Thirst = thirst;
        Hunger = hunger;
        IsFood = isFood;

    }
}
