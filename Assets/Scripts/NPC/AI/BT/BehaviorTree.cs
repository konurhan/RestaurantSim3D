using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorTree : MonoBehaviour
{
    
    private Node root = null;

    protected void Start()
    {
        root = Setup();
    }

    protected void Update()
    {
        if (root != null) root.Evaluate();
    }

    public abstract Node Setup();

}
