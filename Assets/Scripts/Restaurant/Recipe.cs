using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRecipe
{
    string Name { get; set; }
    Dictionary<KeyValuePair<string, int>, int> Ingredients { get; set; }
    int Price { get; set; }
    int Thirst { get; set; }
    int Hunger { get; set; }
    int Quality { get; set; }

    Customer Orderer { get; set; }

    void SetPrice(int newPrice)
    {
        Price = newPrice;
    }

    public void DestroyObject();

    public void ResetStats();
}

public interface IRecipeData
{
    string Name { get; set; }
    Dictionary<KeyValuePair<string, int>, int> Ingredients { get; set; }
    int Price { get; set; }
    int Thirst { get; set; }
    int Hunger { get; set; }
    bool IsFood { get; set; }
}
