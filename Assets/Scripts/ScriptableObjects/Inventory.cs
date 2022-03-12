﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> items = new List<Item>();

    public void AddItem(Item itemToAdd)
    {
        if (items.Count == 0)
        {
            items.Add(itemToAdd);
        }
    }

}
