using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public CarSpawnTable spawnTable;
    public float respawnTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("WaitToSpawnCar");
    }

    private void SpawnCar()
    {
        if (spawnTable != null)
        {
            GameObject spawnTableOutcome = spawnTable.SpawnedCar();
            if (spawnTableOutcome != null)
            {
                Instantiate (spawnTableOutcome, transform.position, Quaternion.identity);
            }
        }
    }

    private IEnumerator WaitToSpawnCar()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(respawnTime);
            SpawnCar();
        }
    }
}
