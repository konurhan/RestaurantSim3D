using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InverterNode : Node
{

    public override NodeState Evaluate()
    {
        NodeState childNS = children[0].Evaluate();
        switch (childNS)
        {
            case NodeState.FAILED:
                nodeState = NodeState.SUCCEED;
                break;
            case NodeState.SUCCEED:
                nodeState = NodeState.FAILED;
                break;
            case NodeState.RUNNING:
                nodeState = NodeState.RUNNING;
                break;
        }
        return nodeState;
    }

    public InverterNode(List<Node> children)
    {
        this.children = children;
        ConnectChildren();
    }

    
}
