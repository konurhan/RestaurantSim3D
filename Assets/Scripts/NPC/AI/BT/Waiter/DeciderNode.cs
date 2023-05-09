using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeciderNode : Node
{
    Waiter waiter;
    public override NodeState Evaluate()
    {
        throw new System.NotImplementedException();
    }


    public DeciderNode(List<Node> children, Waiter waiter)
    {
        this.children = children;
        ConnectChildren();
        this.waiter = waiter;
    }
}
