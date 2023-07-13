using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;

public class GameMaster : MonoBehaviour
{
    public bool isPaused = false;
    [SerializeField] private GameObject UIPause;
    [SerializeField] private GameObject UIGameOver;
    [SerializeField] private GameObject UIGameFinished;
    [SerializeField] private GameObject UICamera;
    [SerializeField] private GameObject ChickenCounterUI;
    [SerializeField] private TextMeshProUGUI chickCounterText;
    [SerializeField] private TextMeshProUGUI TextGameOver;
    [SerializeField] private TextMeshProUGUI TextGameFinished;

    [SerializeField] private int chickCountToFinish = 100;


    private void Start()
    {
        FindObjectOfType<CharacterMovement>().OnScoreUpdated += UpdateScore;
        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown(KeyCode.Escape))
            .Subscribe(_ => HandleEscapeKey())
            .AddTo(this);
        chickCounterText.text = "0";
        ChangeGameSpeed();
    }

    private void UpdateScore(int newChick)
    {
        chickCounterText.text = newChick.ToString();
        LevelManager.Instance.chickCountToCoin = newChick;
        TextGameFinished.text = "Game Finished " + newChick;
        TextGameOver.text = "Game Finished " + newChick;

        if (newChick == chickCountToFinish)
        {
            GameFinished();
            LevelManager.Instance.gameSpeed += 0.2f;
        }
    }

    public void GameFinished()
    {
        isPaused = true;
        UIGameFinished.SetActive(true);
        UIGameOver.SetActive(false);
        UICamera.SetActive(false);
        UIPause.SetActive(false);
        ChickenCounterUI.SetActive(false);

    }

    public void GameOver()
    {
        isPaused = true;
        UIGameFinished.SetActive(false);
        UIGameOver.SetActive(true);
        UICamera.SetActive(false);
        UIPause.SetActive(false);
        ChickenCounterUI.SetActive(false);
    }

    public void GameStart()
    {
        LevelManager.Instance.chickCountToCoin = 0;
        LevelManager.Instance.LoadScene("GameScene");
        ChangeGameSpeed();
    }
    private void ChangeGameSpeed()
    {
        Time.timeScale = LevelManager.Instance.gameSpeed; 
    }

    public void Pause()
    {
        isPaused = true;
        UIGameFinished.SetActive(false);
        UIGameOver.SetActive(false);
        UICamera.SetActive(true);
        UIPause.SetActive(true);
        ChickenCounterUI.SetActive(false);
    }

    public void Resume()
    {
        isPaused = false;
        UIGameFinished.SetActive(false);
        UIGameOver.SetActive(false);
        UICamera.SetActive(false);
        UIPause.SetActive(false);
        ChickenCounterUI.SetActive(true);
    }

    public void ReturnMenu()
    {
        LevelManager.Instance.LoadScene("Menu");
    }
    public void Restart()
    {
        GameStart();
    }



    private void HandleEscapeKey()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
}
