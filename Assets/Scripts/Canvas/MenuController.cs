using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance; 
    public Transform saveLoadCanvas;
    //public Transform hireFireCanvas;
    //private Transform saveTransform;
    //private Transform loadTransform;
    [SerializeField] private List<float> last200Frames;

    public Transform menuPopUp;
    public Transform waitersPopUp;
    public Transform cooksPopUp;
    public Transform ingredientsPopUp;
    public Transform EndOfTheDayPopup;
    public Transform FPSCount;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Invoke(nameof(SetUpMenuElements), 2f);
        Invoke(nameof(SetupCooksPopup), 2f);
        Invoke(nameof(SetupWaitersPopup), 2f);
        Invoke(nameof(SetupIngredientsPopup), 2f);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) saveLoadCanvas.gameObject.SetActive(!saveLoadCanvas.gameObject.activeSelf);//closing and opening the menu
        //if (Input.GetKeyDown(KeyCode.F)) hireFireCanvas.gameObject.SetActive(!hireFireCanvas.gameObject.activeSelf);
        CalculateFPS();
    }

    public void CalculateFPS()
    {
        if (last200Frames.Count < 200)
        {
            last200Frames.Add(Time.deltaTime);
            return;
        }

        last200Frames.Add(Time.deltaTime);
        last200Frames.RemoveAt(0);

        float totalTime = 0f;
        for (int i = 0; i < 200; i++)
        {
            totalTime += last200Frames[i];
        }
        FPSCount.GetComponent<TextMeshProUGUI>().text = "FPS: " + ((int)(200 / totalTime)).ToString();
    }

    public void SetUpMenuElements()//should be called at the start after the menu is loaded from the save file
    {
        Transform recipeParent = menuPopUp.GetChild(0).GetChild(0);
        List<FoodData> fDataList = RestaurantManager.Instance.menu.GetComponent<Menu>().Foods;

        for (int i = 0; i < recipeParent.childCount; i++)
        {
            recipeParent.GetChild(i).gameObject.SetActive(false);
        }

        for (int i=0;i<fDataList.Count;i++)
        {
            recipeParent.GetChild(i).gameObject.SetActive(true);
            FoodData food = fDataList[i];
            Transform recipe = recipeParent.GetChild(i);
            recipe.GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = food.price.ToString();
            recipe.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = food.Name;
            recipe.GetChild(4).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Menu/Images/" + food.Name);
        }

        recipeParent = menuPopUp.GetChild(1).GetChild(0);
        List<DrinkData> dDataList = RestaurantManager.Instance.menu.GetComponent<Menu>().Drinks;

        for (int i = 0; i < recipeParent.childCount; i++)
        {
            recipeParent.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < dDataList.Count; i++)
        {
            recipeParent.GetChild(i).gameObject.SetActive(true);
            DrinkData drink = dDataList[i];
            Transform recipe = recipeParent.GetChild(i);
            recipe.GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = drink.price.ToString();
            recipe.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = drink.Name;
            recipe.GetChild(4).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Menu/Images/" + drink.Name);
        }
    }

    public void IncreaseFoodPrice(int index)
    {
        Transform recipe = menuPopUp.GetChild(0).GetChild(0).GetChild(index);
        string prevPrice = recipe.GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text;
        recipe.GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = (int.Parse(prevPrice) + 1).ToString();
        RestaurantManager.Instance.menu.GetComponent<Menu>().Foods[index].Price++;
        RestaurantManager.Instance.popularity--;
        RestaurantManager.Instance.dailyPopularityChange--;
    }

    public void DecreaseFoodPrice(int index)
    {
        Transform recipe = menuPopUp.GetChild(0).GetChild(0).GetChild(index);
        string prevPrice = recipe.GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text;
        recipe.GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = (int.Parse(prevPrice) - 1).ToString();
        RestaurantManager.Instance.menu.GetComponent<Menu>().Foods[index].Price--;
        RestaurantManager.Instance.popularity++;
        RestaurantManager.Instance.dailyPopularityChange++;
    }
    public void IncreaseDrinkPrice(int index)
    {
        Transform recipe = menuPopUp.GetChild(1).GetChild(0).GetChild(index);
        string prevPrice = recipe.GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text;
        recipe.GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = (int.Parse(prevPrice) + 1).ToString();
        RestaurantManager.Instance.menu.GetComponent<Menu>().Drinks[index].Price++;
        RestaurantManager.Instance.popularity--;
        RestaurantManager.Instance.dailyPopularityChange--;
    }

    public void DecreaseDrinkPrice(int index)
    {
        Transform recipe = menuPopUp.GetChild(1).GetChild(0).GetChild(index);
        string prevPrice = recipe.GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text;
        recipe.GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = (int.Parse(prevPrice) - 1).ToString();
        RestaurantManager.Instance.menu.GetComponent<Menu>().Drinks[index].Price--;
        RestaurantManager.Instance.popularity++;
        RestaurantManager.Instance.dailyPopularityChange++;
    }

    public void LevelUpWaiter(int index)//manual level up upon button press, remove automatic level-up
    {
        Transform waitersParent = waitersPopUp.GetChild(0).GetChild(0);
        Waiter waiter = RestaurantManager.Instance.waiters[index].GetComponent<Waiter>();
        waiter.LevelUp();
        //update values on popup
        Transform slot = waitersParent.GetChild(index);
        slot.GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = waiter.level.ToString();//level
        slot.GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = waiter.wage.ToString();//wage
        slot.GetChild(3).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = waiter.capacity.ToString();//capacity
        slot.GetChild(4).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = waiter.speed.ToString();//speed
    }

    public void LevelUpCook(int index)
    {
        Transform cooksParent = cooksPopUp.GetChild(0).GetChild(0);
        Cook cook = RestaurantManager.Instance.cooks[index].GetComponent<Cook>();
        cook.LevelUp();
        //update values on popup
        Transform slot = cooksParent.GetChild(index);
        slot.GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = cook.level.ToString();//level
        slot.GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = cook.wage.ToString();//wage
        slot.GetChild(3).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = cook.talent.ToString();//talent
        slot.GetChild(4).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = cook.speed.ToString();//speed  
    }

    public void SetupWaitersPopup()
    {
        Transform waitersParent = waitersPopUp.GetChild(0).GetChild(0);
        for (int i = 0; i < waitersParent.childCount; i++)
        {
            waitersParent.GetChild(i).gameObject.SetActive(false);
        }
        List<GameObject> waiters = RestaurantManager.Instance.waiters;
        for (int i = 0; i < waiters.Count; i++)
        {
            waitersParent.GetChild(i).gameObject.SetActive(true);
            Waiter waiter = waiters[i].GetComponent<Waiter>();
            Transform slot = waitersParent.GetChild(i);
            slot.GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = waiter.level.ToString();//level
            slot.GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = waiter.wage.ToString();//wage
            slot.GetChild(3).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = waiter.capacity.ToString();//capacity
            slot.GetChild(4).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = waiter.speed.ToString();//speed
        }
    }

    public void SetupCooksPopup()
    {
        Transform cooksParent = cooksPopUp.GetChild(0).GetChild(0);
        for (int i = 0; i < cooksParent.childCount; i++)
        {
            cooksParent.GetChild(i).gameObject.SetActive(false);
        }
        List<GameObject> cooks = RestaurantManager.Instance.cooks;
        for (int i = 0; i < cooks.Count; i++)
        {
            cooksParent.GetChild(i).gameObject.SetActive(true);
            Cook cook = cooks[i].GetComponent<Cook>();
            Transform slot = cooksParent.GetChild(i);
            slot.GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = cook.level.ToString();//level
            slot.GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = cook.wage.ToString();//wage
            slot.GetChild(3).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = cook.talent.ToString();//talent
            slot.GetChild(4).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = cook.speed.ToString();//speed
        }
    }

    public void SetupIngredientsPopup()
    {
        Transform ingredientsParent = ingredientsPopUp.GetChild(0).GetChild(0);
        for (int i = 0; i < ingredientsParent.childCount; i++)
        {
            ingredientsParent.GetChild(i).gameObject.SetActive(false);
        }
        Dictionary<KeyValuePair<string, int>, int> goods = Storage.instance.StoredGoods;
        int cursor = 0;
        foreach (KeyValuePair< KeyValuePair<string, int>, int> good in goods)
        {
            ingredientsParent.GetChild(cursor).gameObject.SetActive(true);
            Transform slot = ingredientsParent.GetChild(cursor);
            slot.GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = good.Key.Value.ToString();//price
            slot.GetChild(3).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = good.Value.ToString();//amount
            slot.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = good.Key.Key;//name
            //slot.GetChild(5).GetChild(1).GetComponent<Image>().sprite = Resources.Load("/Ingredients/"+ good.Key.Key) as Sprite;
            cursor++;
        }
    }

    public void BuyIngredientByIndex(int index)
    {
        Transform ingredientsParent = ingredientsPopUp.GetChild(0).GetChild(0);
        Transform ingredient = ingredientsParent.GetChild(index);
        string name = ingredient.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text;
        int price = int.Parse(ingredient.GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text);

        for (int i = 0; i < 5; i++)
        {
            RestaurantManager.Instance.BuyIngredient(new KeyValuePair<string, int>(name, price));
        }
        
        int prevAmount = int.Parse(ingredient.GetChild(3).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text);
        //prevAmount++;
        prevAmount += 5;
        ingredient.GetChild(3).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = prevAmount.ToString();
    }

    public void RemoveIngredientByIndex(int index)
    {
        Transform ingredientsParent = ingredientsPopUp.GetChild(0).GetChild(0);
        Transform ingredient = ingredientsParent.GetChild(index);
        string name = ingredient.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text;
        int price = int.Parse(ingredient.GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text);

        RestaurantManager.Instance.ThrowIngredient(new KeyValuePair<string, int>(name, price));
        int prevAmount = int.Parse(ingredient.GetChild(3).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text);
        prevAmount--;
        ingredient.GetChild(3).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = prevAmount.ToString();
    }

    public void OpenMenuPopup()
    {
        menuPopUp.gameObject.SetActive(true);
    }
    public void CloseMenuPopup()
    {
        menuPopUp.gameObject.SetActive(false);
    }
    public void OpenWaitersPopup()
    {
        waitersPopUp.gameObject.SetActive(true);
    }
    public void CloseWaitersPopup()
    {
        waitersPopUp.gameObject.SetActive(false);
    }
    public void OpenCooksPopup()
    {
        cooksPopUp.gameObject.SetActive(true);
    }
    public void CloseCooksPopup()
    {
        cooksPopUp.gameObject.SetActive(false);
    }

    public void OpenIngredientsPopup()
    {
        ingredientsPopUp.gameObject.SetActive(true);
        SetupIngredientsPopup();
    }
    public void CloseIngredientsPopup()
    {
        ingredientsPopUp.gameObject.SetActive(false);
    }
}
