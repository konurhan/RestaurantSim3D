using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MenuData
{
    public List<FoodData> foods;
    public List<DrinkData> drinks;

    public MenuData(Menu menu)
    {
        /*FoodData tempFoodData;
        foreach (Food f in menu.Foods)
        {
            tempFoodData = new FoodData(f);
            foods.Add(tempFoodData);
        }
        DrinkData tempDrinkData;
        foreach (Drink d in menu.Drinks)
        {
            tempDrinkData = new DrinkData(d);
            drinks.Add(tempDrinkData);
        }*/
        foods = menu.Foods;
        drinks = menu.Drinks;
    }
    public MenuData()
    {
        foods= new List<FoodData>();
        drinks= new List<DrinkData>();
    }
}
