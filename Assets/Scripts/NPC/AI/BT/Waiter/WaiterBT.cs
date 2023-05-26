using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class WaiterBT : BehaviorTree
{
    Waiter NPCController;
    new private void Start()
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
                new GoToCustomerTask(NPCController),
            }),
            //deliver order sequence
            new SequenceNode(new List<Node>
            {
                new readyOrdersCheck(NPCController),
                new GoToKitchenTask(NPCController),
                new RecieveOrdersTask(NPCController),
                new CarryOrdersToCustomersTask(NPCController)
            })
        });
        return root;
    }
}
