using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSpawner : MonoBehaviour
{
    private LevelCompleted levelTracker;
    public float spawnHeight = 3;
    public float spawnRadius = 50;

    public GameObject robotPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject robotPrefab = Resources.Load<GameObject>("Robot");
        Vector3 starshipLocation = GameObject.Find("Luminaris Starship").transform.position;

        levelTracker = GameObject.Find("LevelTracker").GetComponent<LevelCompleted>();
        foreach (string level in levelTracker.levelCompleted.Keys)
        {
            // spawn robot for each completed level
            if (levelTracker.levelCompleted[level])
            {
                float randX = Random.value * spawnRadius;
                float randZ = Random.value * spawnRadius;
                Vector3 spawnPosition = starshipLocation + new Vector3(randX, spawnHeight, randZ);
                GameObject spawnRobot = Instantiate(robotPrefab, spawnPosition, Quaternion.identity);
                TextAbove textAbove = spawnRobot.AddComponent<TextAbove>();
                textAbove.Text = level;
                textAbove.OptionalPlayerAppearRadius = 50;
                textAbove.offset.y += 1;
                textAbove.scale = 0.05f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
