using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOverTime : MonoBehaviour
{
    public int TimeBetweenSpawns;
    public TimeSpan timeBetweenSpawns;
    public DateTime nextSpawn = TimeManagement.CurrentTime();
    public GameObject prefab;
    public GameObject spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenSpawns = new TimeSpan(0, 0, TimeBetweenSpawns);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (nextSpawn < TimeManagement.CurrentTime())
        {
            Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity);
            nextSpawn = TimeManagement.CurrentTime() + timeBetweenSpawns;
        }
    }
}
