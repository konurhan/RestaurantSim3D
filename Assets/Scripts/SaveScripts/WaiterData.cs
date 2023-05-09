using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaiterData
{
    public int xp;
    public int level;
    public float speed;
    public int capacity;
    public int wage;

    public WaiterData(Waiter waiter) 
    {
        xp= waiter.xp;
        level= waiter.level;
        speed= waiter.speed;
        capacity= waiter.capacity;
        wage= waiter.wage;
    }

    public WaiterData(int xp, int level, int speed, int capacity, int wage)
    {
        this.xp = xp;
        this.level = level;
        this.speed = speed;
        this.capacity = capacity;
        this.wage = wage;
    }
}
