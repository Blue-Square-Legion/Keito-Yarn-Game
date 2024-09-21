using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class HighScoresMenu : MonoBehaviour
{
    [SerializeField] private BestTimeSO bestTimeSO;
    [SerializeField] private TextMeshProUGUI starsNormalLivingRoom, starsNormalKitchen, starsChallengeLivingRoom, starsChallengeKitchen, starsPuzzleLivingRoom, starsPuzzleKitchen;

    void Start()
    {
        bestTimeSO.LoadHighScores();

        UpdateStarsUI();
    }

    private int StarInterval(int levelIndex) {
        switch(levelIndex) {
            case 1:
                return 40;
            case 2:
                return 50;
            case 3:
                return 60;
            default:
                return 60;
        }
    }

    private int StarRating(int completionTime, int levelIndex) {
        if (completionTime < 1) {
            return 0;
        }
        int starInterval = StarInterval(levelIndex);
        if (completionTime < starInterval) {
            return 3;
        }
        if (completionTime < starInterval * 2) {
            return 2;
        }
        if (completionTime < starInterval * 4) {
            return 1;
        }
        return 0;
    }

    private string GetStarDisplay(int starCount) {
        string stars = "";
        for (int i = 0; i < starCount; i++) {
            stars += "★";
        }
        for (int i = starCount; i < 3; i++) {
            stars += "☆";
        }
        return stars;
    }

    public static int GetSceneIndex(string sceneName)
    {
#if UNITY_EDITOR
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            var scene = EditorBuildSettings.scenes[i];
            if (scene.path.EndsWith(sceneName + ".unity"))
            {
                return i;
            }
        }
#else
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            if (name == sceneName)
            {
                return i;
            }
        }
#endif

        Debug.LogError($"Scene '{sceneName}' does not exist in the build settings.");
        return -1;
    }

    private void UpdateStarsUI()
    {
        int sceneIndex;
        int bestTime;

        sceneIndex = GetSceneIndex("NormalLivingRoomLevel");
        bestTime = bestTimeSO.highScores.Find(score => score.sceneIndex == sceneIndex)?.bestTime ?? -1;
        starsNormalLivingRoom.text = GetStarDisplay(bestTime != -1 ? StarRating(bestTime, sceneIndex) : 0);

        sceneIndex = GetSceneIndex("NormalKitchenLevel");
        bestTime = bestTimeSO.highScores.Find(score => score.sceneIndex == sceneIndex)?.bestTime ?? -1;
        starsNormalKitchen.text = GetStarDisplay(bestTime != -1 ? StarRating(bestTime, sceneIndex) : 0);

        sceneIndex = GetSceneIndex("ChallengeLivingRoomLevel");
        bestTime = bestTimeSO.highScores.Find(score => score.sceneIndex == sceneIndex)?.bestTime ?? -1;
        starsChallengeLivingRoom.text = GetStarDisplay(bestTime != -1 ? StarRating(bestTime, sceneIndex) : 0);

        sceneIndex = GetSceneIndex("ChallengeKitchenLevel");
        bestTime = bestTimeSO.highScores.Find(score => score.sceneIndex == sceneIndex)?.bestTime ?? -1;
        starsChallengeKitchen.text = GetStarDisplay(bestTime != -1 ? StarRating(bestTime, sceneIndex) : 0);

        sceneIndex = GetSceneIndex("PuzzleLivingRoomLevel");
        bestTime = bestTimeSO.highScores.Find(score => score.sceneIndex == sceneIndex)?.bestTime ?? -1;
        starsPuzzleLivingRoom.text = GetStarDisplay(bestTime != -1 ? StarRating(bestTime, sceneIndex) : 0);

        sceneIndex = GetSceneIndex("PuzzleKitchenLevel");
        bestTime = bestTimeSO.highScores.Find(score => score.sceneIndex == sceneIndex)?.bestTime ?? -1;
        starsPuzzleKitchen.text = GetStarDisplay(bestTime != -1 ? StarRating(bestTime, sceneIndex) : 0);
    }
}
