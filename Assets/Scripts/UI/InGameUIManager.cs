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
    [SerializeField] PlayerPrefSO masterSO, musicSO, soundSO, _BestTimePlayerPref;
    [SerializeField] private TextMeshProUGUI endTimeText, bestTimeText, currTimeText;
    [SerializeField] private ScoreProgressController _scoreController;
    

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
        
        AkSoundEngine.SetState("GameStates", "Game_over");
        OnGameEnd.Invoke();
    }

    public void OnResetSettings()
    {
        PlayerPrefs.DeleteKey(masterSO.currKey.ToString());
        PlayerPrefs.DeleteKey(musicSO.currKey.ToString());
        PlayerPrefs.DeleteKey(soundSO.currKey.ToString());
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
            _gameManager.BestTime = _gameManager.CurrentTime;

            PlayerPrefs.SetInt(_BestTimePlayerPref.currKey.ToString(), _gameManager.BestTime);
            PlayerPrefs.Save();

            CancelInvoke("Timer");

            endTimeText.text = string.Format("Final Time: {0}", ConvertTime(_gameManager.CurrentTime));
            bestTimeText.text = string.Format("Best Time: {0}", ConvertTime(_gameManager.BestTime));

            _gameManager.OnGameEnd.Invoke();
        }
    }

    public void ChangeProgressBar()
    {
        _scoreController.SetScore((int)_gameManager.Score);
    }
}
