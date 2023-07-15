using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


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

    private void Awake()
    {
        cooking = false;
        currentOrder = null;
        preparedRecipe = null;
    }

    private void Start()
    {
        
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
        if (CanLevelUp()) 
        { 
            level++; speed++; talent++; wage++; 
            Debug.Log("a waiter has leveled up");

            NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
            agent.speed++;
            agent.acceleration += 5;
            agent.angularSpeed += 10;
        }
        else { Debug.Log("cook do not have enough xp's to level up"); }
    }
    public void PrepareOrder(CustomerOrder customerOrder)//If a cook is idle and orderQueue is not empty call this method
    {
        int quality = Random.Range(1, 20) * talent;
        IRecipeData recipeData = customerOrder.data;
        Customer orderer = customerOrder.orderer;
       
        if (recipeData.IsFood)
        {
            GameObject foodObject = ObjectPooling.Instance.GetPooledRecipe(true,recipeData.Name);
            foodObject.SetActive(true);
            Debug.Log("food  object name is: "+foodObject.name);
            foodObject.GetComponent<Food>().SetValues(recipeData);
            foodObject.GetComponent<Food>().Quality= quality;
            foodObject.GetComponent<Food>().Orderer= orderer;
            preparedRecipe = foodObject;        
        }
        else
        {
            GameObject drinkObject = ObjectPooling.Instance.GetPooledRecipe(false, recipeData.Name);
            drinkObject.SetActive(true);
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

        NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = speed + 3;
        agent.acceleration = 6 + 5 * (speed - 1);
        agent.angularSpeed = 180 + 10 * (speed - 1);
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

    public void ResetForTheNewDay()
    {
        if (preparedRecipe)
        {
            ObjectPooling.Instance.SetPooledRecipe(preparedRecipe);
        }
        cooking = false;
        delivering = false;
        preparedRecipe = null;
        currentOrder = null;
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetBool("isCooking", false);
        animator.SetBool("isCarrying", false);
        animator.SetBool("isWalking", false);
    }
}
