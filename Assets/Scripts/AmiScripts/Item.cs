using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Item
{
    public string Name { get; private set; }

    public int Price { get; set; }

    public Sprite Sprite { get; set; }

    public Item(string name, int price)
    {
        Name = name;
        Price = price;
        Sprite = null;
    }
}

