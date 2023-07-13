using GLTF.Schema;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;


    public bool isPaused = false;
    [SerializeField] private GameObject UIPause;
    [SerializeField] private GameObject UIGameOver;
    [SerializeField] private GameObject UIGameFinished;
    [SerializeField] private GameObject UICamera;
    [SerializeField] private GameObject ChickenCounterUI;
    [SerializeField] private TextMeshProUGUI chickCounterText;
    [SerializeField] private TextMeshProUGUI TextGameOver;
    [SerializeField] private TextMeshProUGUI TextGameFinished;

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Slider _progressBar;

    [SerializeField] private int chickCountToFinish = 100;

    private float gameSpeed = 1f; 
    public int chickCountToCoin = 0;
    public int coin = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        Application.targetFrameRate = 60;
        
    }

    private void Start()
    {
        FindObjectOfType<CharacterMovement>().OnScoreUpdated += UpdateScore;
        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown(KeyCode.Escape))
            .Subscribe(_ => HandleEscapeKey())
            .AddTo(this);
        chickCounterText.text = "0";
    }

    private void UpdateScore(int newChick)
    {
        chickCounterText.text = newChick.ToString();
        chickCountToCoin = newChick;
        TextGameFinished.text = "Game Finished " + newChick;
        TextGameOver.text = "Game Finished " + newChick;

        if (newChick == chickCountToFinish)
        {
            GameFinished();
            gameSpeed += 0.2f;
        }
    }

    public async void LoadScene(string sceneName)
    {
        coin += chickCountToCoin;
        chickCountToCoin = 0;
        var scene= SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        _loaderCanvas.SetActive(true);

        do
        {
            print(scene.progress);
            _progressBar.value = scene.progress;

        } while (scene.progress < 0.9f);;

        await Task.Delay(1000);

        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive (false);
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
        chickCountToCoin = 0;
        LoadScene("GameScene");
        ChangeGameSpeed();
    }

    private void ChangeGameSpeed()
    {
        Time.timeScale = gameSpeed; ;
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
