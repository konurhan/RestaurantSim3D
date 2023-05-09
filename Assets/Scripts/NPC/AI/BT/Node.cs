using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public enum NodeState
{
    RUNNING,
    FAILED,
    SUCCEED
}

public abstract class Node
{
    protected NodeState nodeState;

    protected Node parent;

    protected List<Node> children;
    public abstract NodeState Evaluate();

    protected void ConnectChildren()
    {
        foreach (Node childNode in children)
        {
            childNode.parent = this;
        }
    }

}
