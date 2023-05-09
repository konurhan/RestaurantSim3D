using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOrderSequence : SequenceNode
{
    
    public TakeOrderSequence(List<Node> children): base(children)
    {
        this.children = children;
    }
}
