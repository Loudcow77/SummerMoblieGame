using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    public GameObject dodgeBlock;
    public GameObject scoreText;

    private float spawnTimer = 1f;

    public float waveTimer = 1f;
    // Start is called before the first frame update
    void Update()
    {
        // Create a loop that repeats the spawning after x amount of time
        if(Time.time >= spawnTimer)
        {
            SpawnBlocks();
            spawnTimer = Time.time + waveTimer;
        }

        if (scoreText.activeInHierarchy == true)
        {
            // Speeds up the rate of block spawns after 20 score is reached
            if (FindObjectOfType<Score>().score >= 20)
            {
                waveTimer = 0.6f;
            }
        }
    }

    void SpawnBlocks()
    {
        // Randomly Select a Spawn Point for a box to fall from
        int randomIndex = Random.Range(0, spawnPoints.Length);

        // Loop through the spawn index 
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            // Find the index selected and spawn the object at that point
            if (randomIndex == i)
            {
                Instantiate(dodgeBlock, spawnPoints[i].position, Quaternion.identity);
            }
        }
    }
}
