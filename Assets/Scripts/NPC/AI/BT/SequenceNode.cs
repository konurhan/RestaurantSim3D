using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : Node
{
    public override NodeState Evaluate()
    {
        foreach (Node node in children)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILED:
                    nodeState = NodeState.FAILED;
                    return nodeState;
                case NodeState.RUNNING:
                    nodeState = NodeState.RUNNING;
                    return nodeState;
                case NodeState.SUCCEED:
                    break;
            }
        }
        nodeState = NodeState.SUCCEED;
        return nodeState;
    }
    public SequenceNode(List<Node> children)
    {
        this.children = children;
        ConnectChildren();
    }
}
