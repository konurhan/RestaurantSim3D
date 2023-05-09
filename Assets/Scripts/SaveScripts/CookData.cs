using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CookData
{
    public int xp;
    public int level;
    public float speed;
    public int talent;
    public int wage;

    public CookData(Cook cook)
    {
        xp = cook.xp;
        level = cook.level;
        speed = cook.speed;
        talent = cook.talent;
        wage = cook.wage;
    }

    public CookData(int xp, int level, int speed, int talent, int wage) 
    { 
        this.xp = xp;
        this.level = level;
        this.speed = speed;
        this.talent = talent;
        this.wage = wage;
    }
}
