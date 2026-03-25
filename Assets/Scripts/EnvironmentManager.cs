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
    public int blocksRemaining;

    public void ResetEnvironment()
    {
        if (spawnedBlocks != null)
        {
            foreach (var block in spawnedBlocks)
            {
                if (block != null) Destroy(block);
            }
        }

        blocksRemaining = blockCount;
        spawnedBlocks = new GameObject[blockCount];
        for (int i = 0; i < blockCount; i++)
        {
            Vector3 localPos = new Vector3(Random.Range(-9f, 9f), 0.5f, Random.Range(-9f, 9f));
            Vector3 worldPos = transform.TransformPoint(localPos);
            spawnedBlocks[i] = Instantiate(redBlockPrefab, worldPos, Quaternion.identity, transform);
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
