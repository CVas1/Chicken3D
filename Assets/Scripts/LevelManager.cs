using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Slider _progressBar;


    public int clownChick = 0;
    public int hatChick = 0;


    public float gameSpeed = 1f;
    public int chickCountToCoin = 0;
    public int coin = 0;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void Start()
    {
        
        Application.targetFrameRate = 60;
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


}
