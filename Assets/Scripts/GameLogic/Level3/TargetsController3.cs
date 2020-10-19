#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsController3 : MonoBehaviour
{
    [SerializeField] private SpawObj[] targets;
    [SerializeField] private Transform[] spawnPositions;

    [SerializeField] private float minTimeBetweenSpawns = 1f;
    [SerializeField] private float maxTimeBetweenSpawns = 2.3f;
    [SerializeField] private int timeBeforeFirstSpawn = 2;
    [SerializeField] private int force_up = 450;

    private float timeBetweenSpawns;
    private bool chooseNum = true;
    private float spawnCounter;

    private Rigidbody rb;


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
            SpawnRandomTargets();
            spawnCounter = 0f;
            chooseNum = false;
        }
    }

    private void SpawnRandomTargets()
    {
        int randomIndex = Random.Range(0, 10);
        int randomPositionToSpawnIndex = Random.Range(0, spawnPositions.Length);
        float yVal;


        //Targets are produced so that the chance of producing a bottle is higher then other targets 
        for (int j = 0; j < targets.Length; j++)
        {
            if (randomIndex >= targets[j].minProbRange && randomIndex <= targets[j].maxProbRange)
            {
                GameObject newTarget = Instantiate(targets[j].spawObj, gameObject.transform);
                newTarget.transform.position = spawnPositions[randomPositionToSpawnIndex].position;
                float randomValue = Random.Range(-30f, 30f);


                if (j == 0) yVal = 180;
                else
                {
                    yVal = randomValue;
                }

                newTarget.transform.Rotate(randomValue, yVal, randomValue);
                rb = newTarget.GetComponent<Rigidbody>();
                rb.AddForce(Vector3.up * force_up);

                break;
            }
        }
    }
}