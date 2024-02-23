using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int numOfYarn, currTime = 0, bestTime = -1;
    private int _currentLocationIndex, _sameSpawnCount = 0;

    [SerializeField, Tooltip("Max # times cat can stay in same spot before force move")]
    private int _maxDuplicateSpawn = 1;

    [SerializeField] private int targetScore = 0;

    [SerializeField, Tooltip("The rate to increase the current time every second")]
    private float timePerSecond = 1f, score, highScore;

    [SerializeField] private string mainMenuSceneName;
    [SerializeField] private TextMeshProUGUI endTimeText, bestTimeText, currTimeText;
    [SerializeField] private TagSO _SpawnPoint;
    [SerializeField] private PlayerPrefSO _BestTimePlayerPref;
    [SerializeField] private Slider scoreSlider;
    [SerializeField] private GameObject scoreColor;

    [SerializeField] private float _scoreMulitplier = 2;

    [SerializeField] private ColorSO[] _colorList;

    public GameObject catGameObject;

    [System.NonSerialized]
    public GameObject[] spawnLocPrefab; //kept public for test case. Now auto grabs based on spawnpoint tag.

    public UnityEvent OnGameEnd = new();

    public float Score
    {
        get { return score; }
        set { score = value; }
    }

    // Records the new high score if the current score exceeds the previous high score
    public float HighScore
    {
        get { return highScore; }
        set { highScore = value > highScore ? score : highScore; }
    }

    // The goal number that the player needs to reach
    public int TargetScore
    {
        get { return targetScore; }
        set { targetScore = value; }
    }

    // Records the current best time if it exceeds the previous best time
    public int BestTime
    {
        get { return bestTime; }
        set { bestTime = bestTime < 0 || value < bestTime ? value : bestTime; }
    }

    public int CurrentTime
    {
        get { return currTime; }
        set { currTime = value; }
    }

    public int NumOfYarn
    {
        get { return numOfYarn; }
        set { numOfYarn = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Sets the best time to the default best time if it doesn't find the key for it
        BestTime = PlayerPrefs.HasKey(_BestTimePlayerPref.currKey.ToString()) ? PlayerPrefs.GetInt(_BestTimePlayerPref.currKey.ToString()) : bestTime;

        spawnLocPrefab = GameObject.FindGameObjectsWithTag(_SpawnPoint.Tag);

        if (spawnLocPrefab.Length == 0)
        {
            Debug.LogError("No Spawn Location Set");
            return;
        }

        _currentLocationIndex = Random.Range(0, spawnLocPrefab.Length);
        catGameObject = Instantiate(catGameObject, spawnLocPrefab[_currentLocationIndex].transform.position, spawnLocPrefab[_currentLocationIndex].transform.rotation);
        CatYarnInteraction CatInteract = catGameObject.GetComponent<CatYarnInteraction>();

        CatInteract.OnCatScored.AddListener(UpdateScore);
        UpdateCatColor();

        InvokeRepeating("Timer", 1f, timePerSecond);
    }

    /// <summary>
    /// Changes the cats location pseudorandomly
    /// </summary>
    public void ChangeCatLocation()
    {
        if (spawnLocPrefab.Length <= 1)
        {
            Debug.LogWarning("No Available Spawn Location to Move");
            return;
        }

        int randInt = Random.Range(0, spawnLocPrefab.Length);

        if (randInt == _currentLocationIndex)
        {
            _sameSpawnCount++;
        }

        if (_sameSpawnCount > _maxDuplicateSpawn)
        {
            randInt = GetNewSpawnIndex();
            _sameSpawnCount = 0;
        }

        catGameObject.transform.position = spawnLocPrefab[randInt].transform.position;
        catGameObject.transform.rotation = spawnLocPrefab[randInt].transform.rotation;

        UpdateCatColor();
        _currentLocationIndex = randInt;
    }

    /// <summary>
    /// Gets new Random Spawn Index. 
    /// Excludes prev value by reducing range-1 & incrementing every index >= prev value
    /// ex. [0,1,2,3] -> exclude 1 -> [(0=0),(1=2),(2=3)]
    /// </summary>
    /// <returns></returns>
    private int GetNewSpawnIndex()
    {
        int randInt = Random.Range(0, spawnLocPrefab.Length - 1);

        if (randInt >= _currentLocationIndex)
        {
            randInt++;
        }

        return randInt;
    }

    private void UpdateCatColor()
    {
        ColorSO color = GetRandomColor();
        catGameObject.GetComponent<CatYarnInteraction>().FavoriteColor = color;
        catGameObject.GetComponent<Renderer>().material.color = color.Color;
    }

    private ColorSO GetRandomColor()
    {
        int RandInt = Random.Range(0, _colorList.Length);
        return _colorList[RandInt];
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        InputManager.SwitchControls(ControlMap.UI);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        InputManager.SwitchControls(ControlMap.Player);
    }

    public void LoadMainMenu()
    {
        StaticUIFunctionality.GoToSceneByName(mainMenuSceneName);
    }

    public void RestartGame()
    {
        InputManager.SwitchControls(ControlMap.Player);
        StaticUIFunctionality.GoToSceneByName(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Timer for counting how much time has passed. Also records best time if it is less than current time.
    /// </summary>
    public void Timer()
    {
        currTime++;
        currTimeText.text = "Time Past: " + currTime;

        if (score >= targetScore)
        {
            BestTime = currTime;

            PlayerPrefs.SetInt(_BestTimePlayerPref.currKey.ToString(), BestTime);
            PlayerPrefs.Save();

            CancelInvoke("Timer");

            endTimeText.text = string.Format("Final Time: {0}", CurrentTime);
            bestTimeText.text = string.Format("Best Time: {0}", BestTime);

            OnGameEnd.Invoke();
        }
    }

    public void UpdateScore(float value, bool isFavoriteColor)
    {
        //TODO - add score for favorite

        //Score based on Suika scoring.
        float scaledValue = value * _scoreMulitplier;
        int scoreVal = Mathf.Max(1, (int)(scaledValue * (scaledValue + 1) / 2));

        score += scoreVal;
        highScore = Mathf.Max(score, highScore);

        scoreSlider.value = score / targetScore;

        if (scoreSlider.value < 0.25f)
            scoreColor.GetComponent<Image>().color = Color.red;
        else if (scoreSlider.value > 0.25f && scoreSlider.value < 0.75f)
            scoreColor.GetComponent<Image>().color = Color.yellow;
        else if (scoreSlider.value > 0.75f && scoreSlider.value < 0.99f)
            scoreColor.GetComponent<Image>().color = Color.green;
        else
            scoreColor.GetComponent<Image>().color = Color.blue;

        ChangeCatLocation();
    }
}
