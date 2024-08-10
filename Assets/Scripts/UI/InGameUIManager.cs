using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using Manager.Score;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] GameObject _pauseUI;
    [SerializeField] GameObject _settingsUI;
    [SerializeField] GameObject _confirmationUI;
    [SerializeField] GameObject _gameOverUI;
    [SerializeField] OffScreenIndicator _indicator;
    [SerializeField] Button _tryAgainButton;
    [SerializeField] PlayerPrefSO masterSO, musicSO, soundSO, mouseSO;
    [SerializeField] BestTimeSO bestTimeSO;
    [SerializeField] private TextMeshProUGUI tryAgainButtonText, gameOverText, endTimeText, bestTimeText, currTimeText, ballsLeftText;
    [SerializeField] private TextMeshProUGUI endStarsText, bestStarsText;
    [SerializeField] private ScoreProgressController _scoreController;
    
    private bool countdownStarted = false;
    private int countdownEnd;
    public UnityEvent OnPauseMenuOpen, OnPauseMenuClose, OnGameEnd;

    
    void Start()
    {
        if (!_gameManager) _gameManager = FindObjectOfType<GameManager>();
        _pauseUI.SetActive(false);
        _settingsUI.SetActive(false);
        _confirmationUI.SetActive(false);
        _gameOverUI.SetActive(false);

        _scoreController.Total = _gameManager.TargetScore;

        if (currTimeText)
        {
            InvokeRepeating("Timer", 1f, _gameManager.TimePerSecond);
        }
        else
        {
            Debug.LogWarning("Missing UI Reference: Timer");
        }
        bestTimeSO.LoadHighScores();
    }

    private void OnEnable()
    {
        _gameManager._score.OnScore.AddListener(HandleScoreData);

        _gameManager.OnCatSpawn.AddListener(_indicator.UpdateTarget);
        _gameManager.OnGameEnd.AddListener(HandleGameEnd);

        InputManager.Input.Player.Menu.performed += Menu_performed;
        InputManager.Input.UI.Cancel.performed += Cancel_performed;
    }

    private void OnDisable()
    {
        _gameManager._score.OnScore.RemoveListener(HandleScoreData);

        _gameManager.OnCatSpawn.RemoveListener(_indicator.UpdateTarget);
        _gameManager.OnGameEnd.RemoveListener(HandleGameEnd);

        InputManager.Input.Player.Menu.performed -= Menu_performed;
        InputManager.Input.UI.Cancel.performed -= Cancel_performed;
    }

    private void HandleScoreData(ScoreData data)
    {
        ChangeProgressBar();
    }

    private void Cancel_performed(InputAction.CallbackContext obj)
    {
        Cancel();
    }

    public void Cancel()
    {
        if (_settingsUI.activeSelf)
        {
            OnSettingsClose();
        }
        else if (_confirmationUI.activeSelf)
        {
            OnCloseConfirmation();
        }
        else if (!_pauseUI.activeSelf)
        {
            OnPauseGame();
        }
        else
        {
            OnResumeGame();
        }
    }

    private void Menu_performed(InputAction.CallbackContext obj)
    {
        OnPauseGame();
    }

    private void UpdateEndGameUI()
    {
        _tryAgainButton.onClick.RemoveListener(OnResetGame);
        _tryAgainButton.onClick.RemoveListener(OnLevelChange);
        _tryAgainButton.onClick.RemoveListener(OnBackToMainMenu);

        if (_gameManager.Score < _gameManager.TargetScore) {
            gameOverText.text = "Game Over!";
            tryAgainButtonText.text = "Play Again";
            _tryAgainButton.onClick.AddListener(OnResetGame);
        } else {
            gameOverText.text = "Level Complete!";
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if(nextLevelIndex < sceneCount) {
                tryAgainButtonText.text = "Next Level";
                _tryAgainButton.onClick.AddListener(OnLevelChange);
            } else {
                tryAgainButtonText.text = "Back to Main Menu";
                _tryAgainButton.onClick.AddListener(OnBackToMainMenu);
            }
        }
        UpdateStarUI();
    }

    private int StarInterval() {
        int currLevel = SceneManager.GetActiveScene().buildIndex;
        if(currLevel == 1) {
            return 40;
        } else if(currLevel == 2) {
            return 50;
        } else if(currLevel == 3){
            return 60;
        }
        return 60;
    }

    private int StarRating(int completionTime) {
        if(completionTime < 1) {
            return 0;
        }
        int starInterval = StarInterval();
        if(completionTime < starInterval) {
            return 3;
        }
        if(completionTime < starInterval * 2) {
            return 2;
        }
        if(completionTime < starInterval * 4) {
            return 1;
        }
        return 0;
    }

    private void UpdateStarUI() {
        int i = 0;
        endStarsText.text = "";
        bestStarsText.text = "";
        if(_gameManager.Score < _gameManager.TargetScore) {
            endStarsText.text = "☆☆☆";
        } else {
            while(i < StarRating(_gameManager.CurrentTime)) {
                endStarsText.text += "★";
                i++;
            }
            while(i < 3) {
                endStarsText.text += "☆";
                i++;
            }
        }
        i = 0;
        while(i < StarRating(bestTimeSO.GetBestTimeForCurrentScene())) {
            bestStarsText.text += "★";
            i++;
        }
        while(i < 3) {
            bestStarsText.text += "☆";
            i++;
        }
    }

    public void UpdateBallsLeft(int balls) {
        if(balls > 0) {
            ballsLeftText.text = "Balls Left: " + balls;
        } else {
            countdownStarted = true;
            countdownEnd = _gameManager.CurrentTime + 5;
        }
    }

    public void OnPauseGame()
    {
        OnPauseMenuOpen.Invoke();
        _pauseUI.SetActive(true);
        _gameManager.PauseGame();
      //  AkSoundEngine.SetState("GameStates", "Pause_State");
    }

    public void OnResetGame()
    {
        _gameOverUI.SetActive(false);
        _gameManager.ResumeGame();
        _gameManager.RestartGame();
    }

    public void OnLevelChange(int index)
    {
        _gameManager.ResumeGame();
        AkSoundEngine.StopAll();
        SceneManager.LoadScene(index);
    }

    public void OnLevelChange(string sceneName)
    {
        _gameManager.ResumeGame();
        AkSoundEngine.StopAll();
        SceneManager.LoadScene(sceneName);
    }

    private void OnLevelChange()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        _gameManager.ResumeGame();
        AkSoundEngine.StopAll();
        SceneManager.LoadScene(nextLevelIndex);
    }

    public void OnResumeGame()
    {
        OnPauseMenuClose.Invoke();
        _gameManager.ResumeGame();
      //  AkSoundEngine.SetState("GameStates", "IngameState");
        _pauseUI.SetActive(false);
    }

    public void OnSettingsOpen()
    {
        _settingsUI.SetActive(true);
        _pauseUI.SetActive(false);
    }

    public void OnSettingsClose()
    {
        _pauseUI.SetActive(true);
        _settingsUI.SetActive(false);
    }

    public void OnOpenConfirmation()
    {
        _confirmationUI.SetActive(true);
        _pauseUI.SetActive(false);

    }

    public void OnCloseConfirmation()
    {
        _pauseUI.SetActive(true);
        _confirmationUI.SetActive(false);
    }

    public void OnBackToMainMenu()
    {
        _gameManager.ResumeGame();
        _gameManager.LoadMainMenu();
    }

    public void HandleGameEnd()
    {
        _gameManager.PauseGame();
        _gameOverUI.SetActive(true);
        
        UpdateEndGameUI();
        
        AkSoundEngine.SetState("GameStates", "Game_over");
        OnGameEnd.Invoke();
    }

    public void OnResetSettings()
    {
        PlayerPrefs.DeleteKey(masterSO.currKey.ToString());
        PlayerPrefs.DeleteKey(musicSO.currKey.ToString());
        PlayerPrefs.DeleteKey(soundSO.currKey.ToString());
        PlayerPrefs.DeleteKey(mouseSO.currKey.ToString());
    }


    private string ConvertTime(int sec)
    {
        return System.TimeSpan.FromSeconds(sec).ToString(@"m\:ss");
    }

    /// <summary>
    /// Timer for counting how much time has passed. Also records best time if it is less than current time.
    /// </summary>
    public void Timer()
    {
        _gameManager.CurrentTime++;
        currTimeText.text = ConvertTime(_gameManager.CurrentTime);

        if (_gameManager.Score >= _gameManager.TargetScore)
        {
            bestTimeSO.SetBestTimeForCurrentScene(_gameManager.CurrentTime);

            endTimeText.text = string.Format("Final Time: {0} ", ConvertTime(_gameManager.CurrentTime));
            StopTimer();
        }

        if (countdownStarted) {
            int secondsRemaining = countdownEnd - _gameManager.CurrentTime;
            ballsLeftText.text = "Countdown: " + secondsRemaining;
            if(secondsRemaining <= 0) {
                endTimeText.text = "Final Time: -:-- ";
                StopTimer();
            }
        }
    }

    private void StopTimer()
    {
        CancelInvoke("Timer");
        int bestTimeForCurrentLevel = bestTimeSO.GetBestTimeForCurrentScene();
        if(bestTimeForCurrentLevel > 0) {
            bestTimeText.text = string.Format("Best Time: {0} ", ConvertTime(bestTimeForCurrentLevel));
        } else {
            bestTimeText.text = "Best Time: -:-- ";
        }
        _gameManager.OnGameEnd.Invoke();
    }

    public void ChangeProgressBar()
    {
        _scoreController.SetScore((int)_gameManager.Score);
    }
}
