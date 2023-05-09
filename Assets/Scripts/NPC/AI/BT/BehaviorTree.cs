using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorTree : MonoBehaviour
{
    
    private Node root = null;
    private int waitTime = 0;

    protected void Start()
    {
        root = Setup();
    }

    protected void Update()
    {
        /*if (waitTime < 100)
        {
            waitTime++;
        }
        else
        {
            waitTime = 0;
            if (root != null) root.Evaluate();
        }*/
        if (root != null) root.Evaluate();
    }

    public abstract Node Setup();

}
