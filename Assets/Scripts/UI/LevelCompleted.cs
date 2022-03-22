using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleted : MonoBehaviour
{
    public Dictionary<string, bool> levelCompleted;

    private static LevelCompleted singleton;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(this);
        } else if (singleton != this)
        {
            Destroy(this);
        }
    }

    void Start()
    {
        levelCompleted = new Dictionary<string, bool>();
    }

    // Update is called once per frame
    void Update()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (!levelCompleted.ContainsKey(currentSceneName))
        {
            levelCompleted.Add(currentSceneName, false);
        }

        if (levelCompleted.ContainsKey(currentSceneName) && !levelCompleted[currentSceneName])
        {
            GameObject scorebox = GameObject.Find("ScoreBox");
            ScoreManager scoreManager = scorebox == null ? null : scorebox.GetComponent<ScoreManager>();
            GameObject robot = GameObject.Find("Robot");
            Robots robotScript = robot == null ? null : robot.GetComponent<Robots>();

            if (scoreManager != null && robotScript != null)
            {
                if (scoreManager.score == scoreManager.total && robotScript.isHitted)
                {
                    levelCompleted[currentSceneName] = true;
                }
            }
        }
    }
}
