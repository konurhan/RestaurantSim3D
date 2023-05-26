using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices.ComTypes;
using System.Collections.Generic;
using UnityEditor;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine.Tilemaps;

public static class SaveSystem
{
    public static void SaveCooks(List<Cook> cooks)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/cooks.dat";
        FileStream stream;
        if (File.Exists(path))
        {
            stream = new FileStream(path, FileMode.Open, FileAccess.Write);
        }
        else
        {
            stream = new FileStream(path, FileMode.Create, FileAccess.Write);
        }

        CookList data = new CookList(cooks);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static List<CookData> LoadCooks()
    {
        string path = Application.persistentDataPath + "/cooks.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            CookList data = formatter.Deserialize(stream) as CookList;
            stream.Close();
            return data.cooks;
        }
        else
        {
            Debug.Log("Couldn't find cooks save file");
            return null;
        }
    }

    public static List<CookData> LoadCooksNull()
    {
        CookData newCookData= new CookData(0, 0, 1, 1, 1);//creating the initial cookData
        List<CookData> newData = new List<CookData>();
        newData.Add(newCookData);
        return newData;
    }

    public static void SaveWaiters(List<Waiter> waiters)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/waiters.dat";
        FileStream stream;
        if (File.Exists(path))
        {
            stream = new FileStream(path, FileMode.Open, FileAccess.Write);
        }
        else
        {
            stream = new FileStream(path, FileMode.Create, FileAccess.Write);
        }

        WaiterList data = new WaiterList(waiters);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static List<WaiterData> LoadWaiters()
    {
        string path = Application.persistentDataPath + "/waiters.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            WaiterList data = formatter.Deserialize(stream) as WaiterList;
            stream.Close();
            return data.waiters;
        }
        else
        {
            Debug.Log("Couldn't find waiters save file");
            return null;
        }
    }
    public static List<WaiterData> LoadWaitersNull()
    {
        WaiterData newWaiterData = new WaiterData(0, 0, 1, 1, 1);//creating the initial cookData
        List<WaiterData> newData = new List<WaiterData>();
        newData.Add(newWaiterData);
        return newData;
    }

    public static void SaveRestaurantMenu(Menu menu) 
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/menu.dat";
        FileStream stream;
        if (File.Exists(path))
        {
            stream = new FileStream(path, FileMode.Open, FileAccess.Write);
        }
        else
        {
            stream = new FileStream(path, FileMode.Create, FileAccess.Write);
        }
        MenuData data = new MenuData(menu);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static MenuData LoadRestaurantMenu()
    {
        string path = Application.persistentDataPath + "/menu.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            MenuData menuData = formatter.Deserialize(stream) as MenuData;
            stream.Close();
            return menuData;
        }
        else
        {
            Debug.Log("Couldn't find menu save file");
            return null;
        }
    }

    public static MenuData LoadRestaurantMenuNull()
    {
        MenuData newMenuData = new MenuData();
        List<FoodData> newFoodsData = newMenuData.foods;
        List<DrinkData> newDrinksData = newMenuData.drinks;

        KeyValuePair<string, int> ingredient1 = new KeyValuePair<string, int>("onion", 1);
        KeyValuePair<string, int> ingredient2 = new KeyValuePair<string, int>("beef", 1);
        KeyValuePair<string, int> ingredient3 = new KeyValuePair<string, int>("water", 1);
        KeyValuePair<string, int> ingredient4 = new KeyValuePair<string, int>("salt", 1);
        Dictionary<KeyValuePair<string, int>, int> ingredientsFood = new Dictionary<KeyValuePair<string, int>, int>() { 
            {ingredient1,1 },
            {ingredient2,1 },
            {ingredient3,1 },
            {ingredient4,1 },
        };
        FoodData newFood = new FoodData("onionSoup", 0, ingredientsFood, 6, 4, 8, true);
        newFoodsData.Add(newFood);

        ingredient1 = new KeyValuePair<string, int>("orange", 1);
        ingredient2 = new KeyValuePair<string, int>("lemon", 1);
        ingredient3 = new KeyValuePair<string, int>("pineapple", 1);
        ingredient4 = new KeyValuePair<string, int>("watermelon", 1);
        Dictionary<KeyValuePair<string, int>, int> ingredientsDrink = new Dictionary<KeyValuePair<string, int>, int>() {
            {ingredient1,1 },
            {ingredient2,1 },
            {ingredient3,1 },
            {ingredient4,1 },
        };

        DrinkData newDrink = new DrinkData("mixedFruitJuice", 0, ingredientsDrink, 7, 8, 1, false);
        newDrinksData.Add(newDrink);
        return newMenuData;
    }

    public static void SaveIngredientList(Dictionary<string, int> ingredientList)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/ingredientList.dat";
        FileStream stream;
        if (File.Exists(path))
        {
            stream = new FileStream(path, FileMode.Open, FileAccess.Write);
        }
        else
        {
            stream = new FileStream(path, FileMode.Create, FileAccess.Write);
        }
        formatter.Serialize(stream, ingredientList);
        stream.Close();
    }

    public static Dictionary<string, int> LoadIngredientList()
    {
        string path = Application.persistentDataPath + "/ingredientList.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            Dictionary<string, int> ingredientList = formatter.Deserialize(stream) as Dictionary<string, int>;
            stream.Close();
            return ingredientList;
        }
        else
        {
            Debug.Log("Couldn't find storedGoods save file");
            return null;
        }
    }

    public static Dictionary<string, int> LoadIngredientListNull()//there are no predefined ingredient types in the save file
    {
        Dictionary<string, int> ingredientsStart = new Dictionary<string, int>()//types and prices
        {
            {"salt",1 },{"pepper",1},{"sugar",1 },{"cinnamon",1},{"flour",1 },{"beef",1},{"chicken",1 },{"onion",1},
            {"potato",1 },{"tomato",1 },{"milk",1 },{"water",1 },{"cacao",1},{"honey",1},{"pineapple",1},{"lemon",1},
            {"orange",1 },{"watermelon",1}
        };
        return ingredientsStart;
    }

    public static void SaveStorage(Dictionary<KeyValuePair<string, int>, int> storedGoods)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/storedGoods.dat";
        FileStream stream;
        if (File.Exists(path))
        {
            stream = new FileStream(path, FileMode.Open, FileAccess.Write);
        }
        else
        {
            stream = new FileStream(path, FileMode.Create, FileAccess.Write);
        }
        formatter.Serialize(stream, storedGoods);
        stream.Close();
    }

    public static Dictionary<KeyValuePair<string, int>, int> LoadStorage()
    {
        string path = Application.persistentDataPath + "/storedGoods.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            Dictionary<KeyValuePair<string, int>, int> stats = formatter.Deserialize(stream) as Dictionary<KeyValuePair<string, int>, int>;
            stream.Close();
            return stats;
        }
        else
        {
            Debug.Log("Couldn't find storedGoods save file");
            return null;
        }
    }

    public static Dictionary<KeyValuePair<string, int>, int> LoadStorageNull()//there are no predefined ingredients in the save file
    {
        Dictionary<KeyValuePair<string, int>, int> storageStart = new Dictionary<KeyValuePair<string, int>, int>();
        foreach (KeyValuePair<string,int> item in RestaurantManager.Instance.ingredientList)
        {
            //KeyValuePair<string, int> newIngredient = new KeyValuePair<string, int>(item.Key, item.Value);
            storageStart.Add(item, 40);//40 of each element are given for the start
        }
        return storageStart;
    }

    public static void SaveRestaurantStats(List<int> stats)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/restaurantStats.dat";
        FileStream stream;
        if (File.Exists(path))
        {
            stream = new FileStream(path, FileMode.Open, FileAccess.Write);
        }
        else
        {
            stream = new FileStream(path, FileMode.Create, FileAccess.Write);
        }
        formatter.Serialize(stream, stats);
        stream.Close();
    }

    public static List<int> LoadRestaurantStats()
    {
        string path = Application.persistentDataPath + "/restaurantStats.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            List<int> stats = formatter.Deserialize(stream) as List<int>;
            stream.Close();
            return stats;
        }
        else
        {
            Debug.Log("Couldn't find restaurantStats save file");
            return null;
        }
    }

    public static List<int> LoadRestaurantStatsNull()//there are no predefined restaurant stats in the save file
    {
        List<int> statsStart = new List<int>
        {
            20,//capacity
            20,//popularity
            1000//money
        };
        return statsStart;
    }

}
