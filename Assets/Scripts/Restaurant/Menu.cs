using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Menu : MonoBehaviour 
{
    [SerializeField] private List<FoodData> foods;
    [SerializeField] private List<DrinkData> drinks;
    
    private void Start()
    {
        foods = new List<FoodData>();
        drinks = new List<DrinkData>();
    }
    public List<FoodData> Foods
    {
        get { return foods; }
        set { foods = value; }
    }
    public List<DrinkData> Drinks
    { 
        get { return drinks; }
        set { drinks = value; }
    }
    public void AddFood(FoodData newFood)
    {
        foods.Add(newFood);
    }
    public void AddDrink(DrinkData newDrink)
    {
        drinks.Add(newDrink);
    }
    public void RemoveFood(FoodData oldFood)
    {
        foods.Remove(oldFood);
    }
    public void RemoveDrink(DrinkData oldDrink)
    {
        drinks.Remove(oldDrink);
    }

    public void Load()
    {
        MenuData data = SaveSystem.LoadRestaurantMenu();
        if(data == null) data = SaveSystem.LoadRestaurantMenuNull();
        //else if either data.foods or data.drinks are empty
        foods = data.foods;
        drinks = data.drinks;
    }

    public void Save()
    {
        SaveSystem.SaveRestaurantMenu(this);
    }
}
