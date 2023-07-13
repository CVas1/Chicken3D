using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public bool isPaused = false;
    [SerializeField] private GameObject UIPause;
    [SerializeField] private GameObject UIGameOver;
    [SerializeField] private GameObject UIGameFinished;
    [SerializeField] private GameObject UICamera;
    [SerializeField] private GameObject ChickenCounterUI;
    [SerializeField] private TextMeshProUGUI chickCounterText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI TextGameOver;
    [SerializeField] private TextMeshProUGUI TextGameFinished;
    [SerializeField] private AudioSource soundGameFinished;
    [SerializeField] private AudioSource soundGameOver;
    [SerializeField] private Slider sliderSound;

    [SerializeField] private int chickCountToFinish = 100;


    private void Start()
    {
        FindObjectOfType<CharacterMovement>().OnScoreUpdated += UpdateScore;
        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown(KeyCode.Escape))
            .Subscribe(_ => HandleEscapeKey())
            .AddTo(this);
        chickCounterText.text = "0";
        levelText.text = "Level: " + LevelManager.Instance.level.ToString();
        sliderSound.value = LevelManager.Instance.soundTemp.volume;
        ChangeGameSpeed();
    }

    private void UpdateScore(int newChick)
    {
        chickCounterText.text = newChick.ToString();
        TextGameFinished.text = "Max chick " + newChick;
        TextGameOver.text = "Max chick " + newChick;

        if (newChick == chickCountToFinish)
        {
            GameFinished();
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
        LevelManager.Instance.gameSpeed += .2f;
        LevelManager.Instance.level += 1;
        soundGameFinished.Play();
    }

    public void GameOver()
    {
        isPaused = true;
        UIGameFinished.SetActive(false);
        UIGameOver.SetActive(true);
        UICamera.SetActive(false);
        UIPause.SetActive(false);
        ChickenCounterUI.SetActive(false);
        soundGameOver.Play();
    }

    public void GameStart()
    {
        LevelManager.Instance.soundButtonClick.Play();
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
        LevelManager.Instance.soundButtonClick.Play();

    }

    public void Resume()
    {
        isPaused = false;
        UIGameFinished.SetActive(false);
        UIGameOver.SetActive(false);
        UICamera.SetActive(false);
        UIPause.SetActive(false);
        ChickenCounterUI.SetActive(true);
        LevelManager.Instance.soundButtonClick.Play();

    }

    public void ReturnMenu()
    {
        LevelManager.Instance.soundButtonClick.Play();
        LevelManager.Instance.LoadScene("Menu");
        Time.timeScale = 1f;
    }
    public void Restart()
    {
        LevelManager.Instance.soundButtonClick.Play();
        GameStart();
    }

    public void SoundSet()
    {
        LevelManager.Instance.SoundSet(sliderSound.value);
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
