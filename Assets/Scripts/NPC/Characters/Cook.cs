using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

public class Cook : MonoBehaviour
{
    [SerializeField] public int xp;
    [SerializeField] public int level;
    [SerializeField] public float speed;
    [SerializeField] public int talent;
    [SerializeField] public int wage;

    public CustomerOrder currentOrder;
    public GameObject preparedRecipe;
    public int preperationTime;//calculated seperately for each recipe
    public bool cooking;
    public bool delivering;
    private void Start()
    {
        cooking = false;
        currentOrder = null;
        preparedRecipe = null;
    }
    private void Update()
    {
        
    }

    public bool CanLevelUp()//level up on each 10 preperations
    {
        if(xp >= (level * 10) + 10) { return true; }
        return false;
    }
    public void LevelUp()
    {
        if (CanLevelUp()) { level++; speed++; talent++; wage++; Debug.Log("a waiter has leveled up"); }
        else { Debug.Log("cook do not have enough xp's to level up"); }
    }
    public void PrepareOrder(CustomerOrder customerOrder)//If a cook is idle and orderQueue is not empty call this method
    {
        int quality = Random.Range(1, 20) * talent;
        IRecipeData recipeData = customerOrder.data;
        Customer orderer = customerOrder.orderer;
       
        if (recipeData.IsFood)
        {
            GameObject foodObject = Instantiate(Resources.Load("Menu/Foods/" + recipeData.Name)) as GameObject;
            foodObject.GetComponent<Food>().SetValues(recipeData);
            foodObject.GetComponent<Food>().Quality= quality;
            foodObject.GetComponent<Food>().Orderer= orderer;
            preparedRecipe = foodObject;        
        }
        else
        {
            GameObject drinkObject = Instantiate(Resources.Load("Menu/Drinks/" + recipeData.Name)) as GameObject;
            drinkObject.GetComponent<Drink>().SetValues(recipeData);
            drinkObject.GetComponent<Drink>().Quality = quality;
            drinkObject.GetComponent<Drink>().Orderer = orderer;
            preparedRecipe = drinkObject;
        }
        preparedRecipe.transform.parent = gameObject.transform;//cook parents the order, waiter should take on the parenthood
        preparedRecipe.transform.localPosition = Vector3.forward;
        GainExperience();
        //LevelUp();
    }
    public bool IsEnoughInventory(IRecipeData ordered)
    {
        return Storage.instance.IfEnoughThenAllocate(ordered.Ingredients);
    }
    public void GainExperience()
    {
        xp++;
    }

    public void SetUp(CookData data)
    {
        xp = data.xp;
        level = data.level;
        speed = data.speed;
        talent = data.talent;
        wage = data.wage;
    }

    public bool CompareWithData(CookData data)
    {
        if (data.xp != xp) return false;
        if (data.level != level) return false;
        if (data.speed != speed) return false;
        if (data.talent != talent) return false;
        if (data.wage != wage) return false;
        return true;
    }
}
