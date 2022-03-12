using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[System.Serializable]
public class CarList
{
    public GameObject car;
    public int carChance;
}

[CreateAssetMenu]
public class CarSpawnTable : ScriptableObject
{
    public CarList[] cars;
    public GameObject SpawnedCar()
    {
        int cumProb = 0;
        int currentProb = Random.Range(0, 100);
        for (int i = 0; i < cars.Length; i++)
        {
            cumProb += cars[i].carChance;
            if (currentProb <= cumProb)
            {
                return cars[i].car;
            }
        }

        return null;
    }
}
