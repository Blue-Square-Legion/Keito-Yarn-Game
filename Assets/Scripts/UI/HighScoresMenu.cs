using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class LevelIndexes
{
    public static int NormalLivingRoom = 2;
    public static int NormalKitchen = NormalLivingRoom + 1;
    public static int ChallengeLivingRoom = NormalLivingRoom + 2;
    public static int ChallengeKitchen = NormalLivingRoom + 3;
    public static int PuzzleLivingRoom = NormalLivingRoom + 4;
    public static int PuzzleKitchen = NormalLivingRoom + 5;
}

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

    private void UpdateStarsUI() {
        int bestTimeNormalLivingRoom = bestTimeSO.highScores.Find(score => score.sceneIndex == LevelIndexes.NormalLivingRoom)?.bestTime ?? -1;
        starsNormalLivingRoom.text = GetStarDisplay(bestTimeNormalLivingRoom != -1 ? StarRating(bestTimeNormalLivingRoom, LevelIndexes.NormalLivingRoom) : 0);

        int bestTimeNormalKitchen = bestTimeSO.highScores.Find(score => score.sceneIndex == LevelIndexes.NormalKitchen)?.bestTime ?? -1;
        starsNormalKitchen.text = GetStarDisplay(bestTimeNormalKitchen != -1 ? StarRating(bestTimeNormalKitchen, LevelIndexes.NormalKitchen) : 0);

        int bestTimeChallengeLivingRoom = bestTimeSO.highScores.Find(score => score.sceneIndex == LevelIndexes.ChallengeLivingRoom)?.bestTime ?? -1;
        starsChallengeLivingRoom.text = GetStarDisplay(bestTimeChallengeLivingRoom != -1 ? StarRating(bestTimeChallengeLivingRoom, LevelIndexes.ChallengeLivingRoom) : 0);

        int bestTimeChallengeKitchen = bestTimeSO.highScores.Find(score => score.sceneIndex == LevelIndexes.ChallengeKitchen)?.bestTime ?? -1;
        starsChallengeKitchen.text = GetStarDisplay(bestTimeChallengeKitchen != -1 ? StarRating(bestTimeChallengeKitchen, LevelIndexes.ChallengeKitchen) : 0);

        int bestTimePuzzleLivingRoom = bestTimeSO.highScores.Find(score => score.sceneIndex == LevelIndexes.PuzzleLivingRoom)?.bestTime ?? -1;
        starsPuzzleLivingRoom.text = GetStarDisplay(bestTimePuzzleLivingRoom != -1 ? StarRating(bestTimePuzzleLivingRoom, LevelIndexes.PuzzleLivingRoom) : 0);

        int bestTimePuzzleKitchen = bestTimeSO.highScores.Find(score => score.sceneIndex == LevelIndexes.PuzzleKitchen)?.bestTime ?? -1;
        starsPuzzleKitchen.text = GetStarDisplay(bestTimePuzzleKitchen != -1 ? StarRating(bestTimePuzzleKitchen, LevelIndexes.PuzzleKitchen) : 0);
    }
}
