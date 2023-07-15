using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : Node
{
    public override NodeState Evaluate()
    {
        foreach (Node node in children)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILED:
                    continue;
                case NodeState.RUNNING:
                    nodeState = NodeState.RUNNING;
                    return nodeState;
                case NodeState.SUCCEED:
                    nodeState = NodeState.SUCCEED;
                    return nodeState;
            }
        }
        nodeState = NodeState.FAILED;
        return nodeState;
    }
    public SelectorNode(List<Node> children)
    {
        this.children = children;
        ConnectChildren();
    }
}
