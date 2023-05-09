using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ingredient
{
    private string name;
    private int price;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Price
    {
        get { return price; }
        set { price = value; }
    }

    public Ingredient(string name, int price)
    {
        this.name = name;
        this.price = price;
    }
}


public class Onion : Ingredient
{
    public Onion(int price) : base("onion", price) { }
}

public class Salt : Ingredient
{
    public Salt(int price) : base("salt", price) { }
}

public class Pepper : Ingredient
{
    public Pepper(int price) : base("pepper", price) { }
}

public class Sugar : Ingredient
{
    public Sugar(int price) : base("sugar", price) { }
}

public class Cinnamon : Ingredient
{
    public Cinnamon(int price) : base("cinnamon", price) { }
}

public class Flour : Ingredient
{
    public Flour(int price) : base("flour", price) { }
}

public class Beef : Ingredient
{
    public Beef(int price) : base("beef", price) { }
}

public class Chicken : Ingredient
{
    public Chicken(int price) : base("chicken", price) { }
}

public class Potato : Ingredient
{
    public Potato(int price) : base("potato", price) { }
}

public class Tomato : Ingredient
{
    public Tomato(int price) : base("tomato", price) { }
}

public class Milk : Ingredient
{
    public Milk(int price) : base("milk", price) { }
}

public class Water : Ingredient
{
    public Water(int price) : base("water", price) { }
}

public class Cacao : Ingredient
{
    public Cacao(int price) : base("cacao", price) { }
}

public class Honey : Ingredient
{
    public Honey(int price) : base("honey", price) { }
}

public class Pineapple : Ingredient
{
    public Pineapple(int price) : base("pineapple", price) { }
}

public class Lemon : Ingredient
{
    public Lemon(int price) : base("lemon", price) { }
}

public class Orange : Ingredient
{
    public Orange(int price) : base("orange", price) { }
}

public class Watermelon : Ingredient
{
    public Watermelon(int price) : base("watermelon", price) { }
}