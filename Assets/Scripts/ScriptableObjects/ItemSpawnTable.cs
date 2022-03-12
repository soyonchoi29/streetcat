using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[System.Serializable]
public class ItemList
{
    public Item item;
    public int itemChance;
}

[CreateAssetMenu]
public class ItemSpawnTable : ScriptableObject
{
    public ItemList[] items;
    public Item SpawnedItem()
    {
        int cumProb = 0;
        int currentProb = Random.Range(0, 100);
        for (int i = 0; i < items.Length; i++)
        {
            cumProb += items[i].itemChance;
            if (currentProb <= cumProb)
            {
                return items[i].item;
            }
        }
        return null;
    }
}
