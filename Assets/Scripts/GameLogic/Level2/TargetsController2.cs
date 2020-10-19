#pragma warning disable 649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsController2 : MonoBehaviour
{
    [SerializeField] private SpawObj[] targets;
    [SerializeField] private Transform[] spawnPositions;

    [SerializeField] private int timeBeforeFirstSpawn = 2;
    [SerializeField] private float minTimeBetweenSpawns = 2f;
    [SerializeField] private float maxTimeBetweenSpawns = 5f;

    private float timeBetweenSpawns;
    private bool chooseNum = true;
    private float spawnCounter;

    void Start()
    {
        timeBetweenSpawns = timeBeforeFirstSpawn;
    }

    void Update()
    {
        if (!chooseNum)
        {
            timeBetweenSpawns = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
            chooseNum = true;
        }

        spawnCounter += Time.deltaTime;
        if (spawnCounter >= timeBetweenSpawns)
        {
            //creating targers for 3 posisiton
            for (int i = 0; i < spawnPositions.Length; i++)
            {
                SpawnRandomTargets(i);
            }
            spawnCounter = 0f;
            chooseNum = false;
        }
    }

    private void SpawnRandomTargets(int randomPositionToSpawnIndex)
    {
        int randomIndex = Random.Range(0, 10);

        //Targets are produced so that the chance of producing a bottle is higher then other targets 
        for (int j = 0; j < targets.Length; j++)
        {
            if (randomIndex >= targets[j].minProbRange && randomIndex <= targets[j].maxProbRange)
            {
                GameObject newTarget = Instantiate(targets[j].spawObj, gameObject.transform);
                newTarget.transform.position = spawnPositions[randomPositionToSpawnIndex].position;
                if (j == 0)
                {
                    newTarget.transform.Rotate(0, 235, 0, Space.Self);
                }

                break;
            }
        }
    }
}