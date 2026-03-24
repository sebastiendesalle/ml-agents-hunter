using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
public class EnvironmentManager : MonoBehaviour
{
    public GameObject redBlockPrefab;
    public int blockCount = 5;
    public HunterAgent hunter;
    public PredatorAgent predator;
    private GameObject[] spawnedBlocks;

    public void ResetEnvironment()
    {
        if (spawnedBlocks != null)
        {
            foreach (var block in spawnedBlocks)
            {
                if (block != null) Destroy(block);
            }
        }

        spawnedBlocks = new GameObject[blockCount];
        for (int i = 0; i < blockCount; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-9f, 9f), 1f, Random.Range(-9f, 9f));
            spawnedBlocks[i] = Instantiate(redBlockPrefab, spawnPos, Quaternion.identity, transform);
        }
    }

    public int CountRemainingBlocks()
    {
        int count = 0;
        foreach (var block in spawnedBlocks)
        {
            if (block != null) count++;
        }
        return count;
    }
}
