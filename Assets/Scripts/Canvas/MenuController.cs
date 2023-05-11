using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Transform saveLoadCanvas;
    public Transform hireFireCanvas;
    //private Transform saveTransform;
    //private Transform loadTransform;

    public Transform menuPopUp;
    public Transform waitersPopUp;
    public Transform cooksPopUp;
    public Transform ingredientsPopUp;

    private void Start()
    {
        Invoke(nameof(SetUpMenuElements), 2f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) saveLoadCanvas.gameObject.SetActive(!saveLoadCanvas.gameObject.activeSelf);//closing and opening the menu
        if (Input.GetKeyDown(KeyCode.F)) hireFireCanvas.gameObject.SetActive(!hireFireCanvas.gameObject.activeSelf);
    }

    public void SetUpMenuElements()//should be called at the start after the menu is loaded from the save file
    {
        Transform recipeParent = menuPopUp.GetChild(0).GetChild(0);
        List<FoodData> fDataList = RestaurantManager.Instance.menu.GetComponent<Menu>().Foods;
        for (int i=0;i<fDataList.Count;i++)
        {
            FoodData food = fDataList[i];
            Transform recipe = recipeParent.GetChild(i);
            recipe.GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = food.price.ToString();
            recipe.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = food.Name;
            recipe.GetChild(4).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Menu/Images/" + food.Name);
        }

        recipeParent = menuPopUp.GetChild(1).GetChild(0);
        List<DrinkData> dDataList = RestaurantManager.Instance.menu.GetComponent<Menu>().Drinks;
        for (int i = 0; i < dDataList.Count; i++)
        {
            DrinkData drink = dDataList[i];
            Transform recipe = recipeParent.GetChild(i);
            recipe.GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = drink.price.ToString();
            recipe.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = drink.Name;
            recipe.GetChild(4).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Menu/Images/" + drink.Name);
        }
    }

    public void IncreaseFoodPrice(int index)
    {
        RestaurantManager.Instance.menu.GetComponent<Menu>().Foods[index].Price++;
        RestaurantManager.Instance.popularity--;
    }

    public void DecreaseFoodPrice(int index)
    {
        RestaurantManager.Instance.menu.GetComponent<Menu>().Foods[index].Price--;
        RestaurantManager.Instance.popularity++;
    }

    public void OpenMenuPopup()
    {
        menuPopUp.gameObject.SetActive(true);
    }
    public void CloseMenuPopup()
    {
        menuPopUp.gameObject.SetActive(false);
    }


}
