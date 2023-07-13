using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Slider _progressBar;
    [SerializeField] public AudioSource soundButtonClick;
    [SerializeField] public AudioSource soundBadButtonClick;
    [SerializeField] public AudioSource soundTemp;
    



    public int clownChick = 0;
    public int hatChick = 0;


    public float gameSpeed = 1f;
    public int level = 1;
    
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
        
       
        var scene= SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        _loaderCanvas.SetActive(true);

        do
        {
            _progressBar.value = scene.progress;

        } while (scene.progress < 0.9f);;

        await Task.Delay(1000);

        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive (false);
    }

    public void SoundSet(float volume)
    {
        soundTemp.volume = volume;
    }

}
