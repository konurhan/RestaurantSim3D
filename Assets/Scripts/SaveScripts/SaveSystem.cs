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
        Debug.Log("LoadRestaurantMenuNull is called");
        MenuData newMenuData = new MenuData();
        List<FoodData> newFoodsData = newMenuData.foods;
        List<DrinkData> newDrinksData = newMenuData.drinks;

        KeyValuePair<string, int> ingredient1 = new KeyValuePair<string, int>("onion", 1);
        KeyValuePair<string, int> ingredient2 = new KeyValuePair<string, int>("beef", 1);
        KeyValuePair<string, int> ingredient3 = new KeyValuePair<string, int>("water", 1);
        KeyValuePair<string, int> ingredient4 = new KeyValuePair<string, int>("salt", 1);
        Dictionary<KeyValuePair<string, int>, int> ingredientsFood = new Dictionary<KeyValuePair<string, int>, int>() { 
            {ingredient1,3 },
            {ingredient2,2 },
            {ingredient3,2 },
            {ingredient4,1 },
        };
        FoodData newFood = new FoodData("onionSoup", 0, ingredientsFood, 6, 4, 8, true);
        newFoodsData.Add(newFood);

        ingredient1 = new KeyValuePair<string, int>("onion", 1);
        ingredient2 = new KeyValuePair<string, int>("beef", 1);
        ingredient3 = new KeyValuePair<string, int>("water", 1);
        ingredient4 = new KeyValuePair<string, int>("salt", 1);
        KeyValuePair<string, int> ingredient5 = new KeyValuePair<string, int>("flour", 1);
        KeyValuePair<string, int> ingredient6 = new KeyValuePair<string, int>("tomato", 1);
        ingredientsFood = new Dictionary<KeyValuePair<string, int>, int>() {
            {ingredient1,1 },
            {ingredient2,3 },
            {ingredient3,1 },
            {ingredient4,1 },
            {ingredient5,2 },
            {ingredient6,1 },
        };
        newFood = new FoodData("cheeseBurger", 0, ingredientsFood, 14, 1, 15, true);
        newFoodsData.Add(newFood);

        ingredient1 = new KeyValuePair<string, int>("salt", 1);
        ingredient2 = new KeyValuePair<string, int>("pepper", 1);
        ingredient3 = new KeyValuePair<string, int>("chicken", 1);
        ingredientsFood = new Dictionary<KeyValuePair<string, int>, int>() {
            {ingredient1,1 },
            {ingredient2,1 },
            {ingredient3,3 },
        };
        newFood = new FoodData("chickenDrumsticks", 0, ingredientsFood, 12, 1, 14, true);
        newFoodsData.Add(newFood);

        ingredient1 = new KeyValuePair<string, int>("egg", 1);
        ingredient2 = new KeyValuePair<string, int>("pepper", 1);
        ingredient3 = new KeyValuePair<string, int>("salt", 1);
        ingredientsFood = new Dictionary<KeyValuePair<string, int>, int>() {
            {ingredient1,2 },
            {ingredient2,1 },
            {ingredient3,1 },
        };
        newFood = new FoodData("friedEgg", 0, ingredientsFood, 6, 1, 5, true);
        newFoodsData.Add(newFood);

        ingredient1 = new KeyValuePair<string, int>("egg", 1);
        ingredient2 = new KeyValuePair<string, int>("flour", 1);
        ingredient3 = new KeyValuePair<string, int>("sugar", 1);
        ingredient4 = new KeyValuePair<string, int>("honey", 1);
        ingredient5 = new KeyValuePair<string, int>("milk", 1);
        ingredientsFood = new Dictionary<KeyValuePair<string, int>, int>() {
            {ingredient1,1 },
            {ingredient2,2 },
            {ingredient3,1 },
            {ingredient4,1 },
            {ingredient5,2 },
        };
        newFood = new FoodData("pancakes", 0, ingredientsFood, 11, 1, 10, true);
        newFoodsData.Add(newFood);

        ingredient1 = new KeyValuePair<string, int>("beef", 1);
        ingredient2 = new KeyValuePair<string, int>("salt", 1);
        ingredient3 = new KeyValuePair<string, int>("pepper", 1);
        ingredientsFood = new Dictionary<KeyValuePair<string, int>, int>() {
            {ingredient1,3 },
            {ingredient2,2 },
            {ingredient3,2 },
        };
        newFood = new FoodData("steak", 0, ingredientsFood, 25, 1, 18, true);
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

        ingredient1 = new KeyValuePair<string, int>("water", 1);
        ingredient2 = new KeyValuePair<string, int>("lemon", 1);
        ingredient3 = new KeyValuePair<string, int>("salt", 1);
        ingredientsDrink = new Dictionary<KeyValuePair<string, int>, int>() {
            {ingredient1,2 },
            {ingredient2,1 },
            {ingredient3,1 },
        };
        newDrink = new DrinkData("SparklingWater", 0, ingredientsDrink, 4, 12, 1, false);
        newDrinksData.Add(newDrink);

        ingredient1 = new KeyValuePair<string, int>("milk", 1);
        ingredient2 = new KeyValuePair<string, int>("cacao", 1);
        ingredient3 = new KeyValuePair<string, int>("cinnamon", 1);
        ingredientsDrink = new Dictionary<KeyValuePair<string, int>, int>() {
            {ingredient1,3 },
            {ingredient2,2 },
            {ingredient3,1 },
        };
        newDrink = new DrinkData("MilkShake", 0, ingredientsDrink, 10, 7, 5, false);
        newDrinksData.Add(newDrink);

        ingredient1 = new KeyValuePair<string, int>("water", 1);
        ingredient2 = new KeyValuePair<string, int>("lemon", 1);
        ingredient3 = new KeyValuePair<string, int>("sugar", 1);
        ingredientsDrink = new Dictionary<KeyValuePair<string, int>, int>() {
            {ingredient1,3 },
            {ingredient2,2 },
            {ingredient3,4 },
        };
        newDrink = new DrinkData("Gazoz", 0, ingredientsDrink, 6, 9, 1, false);
        newDrinksData.Add(newDrink);
        //Debug.Log("food and drink from loadmenunull method "+newMenuData.foods[0].Name+" "+ newMenuData.drinks[0].Name);
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
            {"orange",1 },{"watermelon",1},{"egg",1}
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

    public static void SaveTables(List<Vector3[]> transforms)
    {
        /*List<SerializableVector3[]> trans = new List<SerializableVector3[]>();
        for (int i = 0; i < transforms.Count; i++)
        {
            trans[i][0].x = transforms[i][0].x; trans[i][0].y = transforms[i][0].y; trans[i][0].z = transforms[i][0].z; //position
            trans[i][1].x = transforms[i][1].x; trans[i][1].y = transforms[i][1].y; trans[i][1].z = transforms[i][1].z; //eulerangles
        }*/
        List<SerializableVector3[]> trans = Vector3ToStruct(transforms);
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/tables.dat";
        FileStream stream;
        if (File.Exists(path))
        {
            stream = new FileStream(path, FileMode.Open, FileAccess.Write);
        }
        else
        {
            stream = new FileStream(path, FileMode.Create, FileAccess.Write);
        }
        formatter.Serialize(stream, trans);
        stream.Close();
    }

    public static List<Vector3[]> LoadTables()
    {
        List<Vector3[]> returnVal;
        string path = Application.persistentDataPath + "/tables.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            List<SerializableVector3[]> stats = formatter.Deserialize(stream) as List<SerializableVector3[]>;
            stream.Close();
            returnVal = StructToVector3(stats);
            return returnVal;
        }
        else
        {
            Debug.Log("Couldn't find restaurantStats save file");
            return null;
        }
    }

    public static List<Vector3[]> LoadTablesNull()//there are no predefined restaurant stats in the save file
    {
        Vector3 pos1 = new Vector3(25, -1.4f, 24.4f);
        Vector3 pos2 = new Vector3(5, -1.4f, 24.4f);
        Vector3 pos3 = new Vector3(5, -1.4f, 12.5f);
        Vector3 pos4 = new Vector3(25, -1.4f, 12.5f);

        Vector3 localEulerAngles = Vector3.zero;
        Vector3 localScale = new Vector3(1.5f, 1.5f, 1.5f);

        List<Vector3[]> statsStart = new List<Vector3[]>
        {
            new Vector3[3] {pos1, localEulerAngles, localScale},
            new Vector3[3] {pos2, localEulerAngles, localScale},
            new Vector3[3] {pos3, localEulerAngles, localScale},
            new Vector3[3] {pos4, localEulerAngles, localScale}
        };
        return statsStart;
    }

    public static List<SerializableVector3[]> Vector3ToStruct(List<Vector3[]> toConvert)
    {
        List<SerializableVector3[]> converted = new List<SerializableVector3[]>();
        for (int i = 0; i < toConvert.Count; i++)
        {
            converted.Add(new SerializableVector3[toConvert[i].Length]);
            for (int j = 0; j < toConvert[i].Length; j++)
            {
                Debug.Log("Vector3ToStruct (i,j): " + i + " " + j);
                converted[i][j] = new SerializableVector3();
                converted[i][j].x = toConvert[i][j].x; converted[i][j].y = toConvert[i][j].y; converted[i][j].z = toConvert[i][j].z;
            }
        }
        return converted;
    }

    public static List<Vector3[]> StructToVector3(List<SerializableVector3[]> toConvert)
    {
        List<Vector3[]> converted = new List<Vector3[]>();
        for (int i = 0; i < toConvert.Count; i++)
        {
            converted.Add(new Vector3[toConvert[i].Length]);
            for (int j = 0; j < toConvert[i].Length; j++)
            {   
                converted[i][j] = new Vector3();
                converted[i][j].x = toConvert[i][j].x; converted[i][j].y = toConvert[i][j].y; converted[i][j].z = toConvert[i][j].z;
            }
        }
        return converted;
    }
}

[System.Serializable]
public struct SerializableVector3
{
    public float x;
    public float y;
    public float z;
}
