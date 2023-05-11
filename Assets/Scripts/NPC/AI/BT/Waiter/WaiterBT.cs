using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class WaiterBT : BehaviorTree
{
    Waiter NPCController;

    new private void Start()//hide the start method of parent class
    {
        NPCController = gameObject.GetComponent<Waiter>();
        base.Start();
    }
    public override Node Setup()
    {
        Node root = new SelectorNode(new List<Node>
        {
            //take order sequence
            new SequenceNode(new List<Node>
            {
                new isNotDeliveringCheck(NPCController),
                new CurrentCustomerCheck(NPCController),
                //new orderRequestQueueCheck(),
                new GoToCustomerTask(NPCController),
                //new AddOrderTask(NPCController)
            }),

            //deliver order sequence
            new SequenceNode(new List<Node>
            {
                new readyOrdersCheck(NPCController),
                new GoToKitchenTask(NPCController),
                new RecieveOrdersTask(NPCController),//at the end of this task we give the first customer destination
                new CarryOrdersToCustomersTask(NPCController)//test this with more then one customers
            })
        });

        return root;
    }
}
