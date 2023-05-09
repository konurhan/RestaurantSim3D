using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class CustomerBT : BehaviorTree
{
    public Customer NPCController;
    new private void Start()//hide the start method of parent class
    {
        if(gameObject.GetComponent<Customer>() != null)
        {
            Debug.Log("customer script is found.");
            NPCController = gameObject.GetComponent<Customer>();
        }
        else
        {
            Debug.Log("no customer script could be found.");
        }
        base.Start();
    }

    new private void Update()
    {
        base.Update();
        /*Debug.Log("customer gameObject name: " + gameObject.name);
        NPCController = gameObject.GetComponent<Customer>();*/
    }
    public override Node Setup()
    {
        Node root = new SelectorNode(new List<Node>
        {
            //arrival sequence
            new SequenceNode(new List<Node>{
                new InverterNode(new List<Node>
                {
                    new CheckIfSeated(NPCController)//if not seated continue with the taking seat process
                }),
                new CheckForEmptySeat(NPCController),
                new GoToSeatTask(NPCController),
            }),

            //customer is in restaurant sequence
            new SequenceNode(new List<Node>
            {
                new CheckIfSeated(NPCController),
                new SelectorNode(new List<Node>
                {
                    new SequenceNode(new List<Node>
                    {
                        new InverterNode(new List<Node>
                        {
                            new CheckIfOrdered(NPCController)//if already ordered continue with waiting task
                        }),
                        
                        new CallWaiterTask(NPCController),
                        new WaitForWaiterToTakeOrderTask(NPCController),
                        new MakeAnOrderTask(NPCController)
                    }),
                    new SequenceNode(new List<Node>
                    {
                        new CheckIfOrdered(NPCController),//if not ordered leave
                        new WaitForOrderTask(NPCController),//failure of this node directly sends us to leaving sequence
                        new ConsumeTask(NPCController),
                        new InverterNode(new List<Node>//if customer is satisfied, inverted
                        {
                            new CheckIfShouldLeave(NPCController)//invert to make sequence fail if leave check succeeds, so that we go into leaving
                        })
                    })
                }),
                
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
