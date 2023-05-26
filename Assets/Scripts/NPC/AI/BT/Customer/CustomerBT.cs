using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class CustomerBT : BehaviorTree
{
    public Customer NPCController;
    new private void Start()
    {
        NPCController = gameObject.GetComponent<Customer>();
        base.Start();
    }
    public override Node Setup()
    {
        Node root = new SelectorNode(new List<Node>
        {
            //arrival sequence
            new SequenceNode(new List<Node>{
                new InverterNode(new List<Node>
                {
                    new CheckIfSeated(NPCController)
                }),
                new CheckForEmptySeat(NPCController),
                new GoToSeatTask(NPCController),
            }),
            //customer is getting served sequence
            new SequenceNode(new List<Node>
            {
                new CheckIfSeated(NPCController),
                new SelectorNode(new List<Node>
                {
                    new SequenceNode(new List<Node>
                    {
                        new InverterNode(new List<Node>
                        {
                            new CheckIfOrdered(NPCController)
                        }),
                        new CallWaiterTask(NPCController),
                        new WaitForWaiterToTakeOrderTask(NPCController),
                        new MakeAnOrderTask(NPCController)
                    }),
                    new SequenceNode(new List<Node>
                    {
                        new CheckIfOrdered(NPCController),
                        new WaitForOrderTask(NPCController),
                        new ConsumeTask(NPCController),
                        new InverterNode(new List<Node>
                        {
                            new CheckIfShouldLeave(NPCController)
                        })
                    })
                })
            }),
            //leaving sequence
            new SequenceNode(new List<Node>
            {
                new LeaveRestaurant(NPCController),
            })
        });
        return root;
    }
}
