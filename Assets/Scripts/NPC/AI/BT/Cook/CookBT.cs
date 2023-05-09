using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookBT : BehaviorTree
{
    Cook NPCController;
    new private void Start()//hide the start method of parent class
    {
        NPCController = gameObject.GetComponent<Cook>();
        base.Start();
    }

    public override Node Setup()
    {
        Node root = new SelectorNode(new List<Node>
        {
            new SequenceNode(new List<Node>
            {
                new CheckIfIdle(NPCController),
                new GoToKitchenCounterTask(NPCController),
                new CheckForIngredients(NPCController),
            }),

            new SequenceNode(new List<Node> 
            {
                new CheckIfCooking(NPCController),
                new PrepareOrderTask(NPCController),
            }),

            new SequenceNode(new List<Node>
            {
                new CheckIfDelivering(NPCController),
                new CarryOrdersToReadyCounterTask(NPCController)
            })
            
        });



        return root;
    }
}
