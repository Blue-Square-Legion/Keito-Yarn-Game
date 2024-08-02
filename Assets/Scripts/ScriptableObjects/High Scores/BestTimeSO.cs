using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[CreateAssetMenu(fileName = "Best Time", menuName = "SO/Best Time")]
public class BestTimeSO : ScriptableObject
{
    [System.Serializable]
    public class LevelScore
    {
        public int sceneIndex;
        public int bestTime;
    }

    public List<LevelScore> highScores = new List<LevelScore>();

    public int GetBestTimeForCurrentScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        foreach (var levelScore in highScores)
        {
            if (levelScore.sceneIndex == sceneIndex)
            {
                return levelScore.bestTime;
            }
        }

        return -1;
    }

    public void SetBestTimeForCurrentScene(int time)
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        foreach (var levelScore in highScores)
        {
            if (levelScore.sceneIndex == sceneIndex)
            {
                if (time < levelScore.bestTime)
                {
                    levelScore.bestTime = time;
                    SaveHighScores();
                }
                return;
            }
        }

        highScores.Add(new LevelScore { sceneIndex = sceneIndex, bestTime = time });
        SaveHighScores();
    }

    private string filePath => Path.Combine(Application.persistentDataPath, "highscores.json");
    public void SaveHighScores()
    {
        string json = JsonUtility.ToJson(this, true);
        File.WriteAllText(filePath, json);
    }

    public void LoadHighScores()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }
}
