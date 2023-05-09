using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//we should use a navmesh agent here
public class GoToKitchenTask : Node
{
    Waiter waiter;
    public GoToKitchenTask(Waiter waiter)
    {
        this.waiter = waiter;
    }
    public override NodeState Evaluate()
    {
        throw new System.NotImplementedException();
        /*
         if(not arrived to kitchen)
            move by Time.deltaTime * speed
            return Running
         else
            return Success
         
         
         */
    }
}
