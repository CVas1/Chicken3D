using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    [SerializeField] private GameObject UIPanel;
    [SerializeField] private GameObject UICamera;
    [SerializeField] private GameObject ChickenCounter;




    void Start()
    {
        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown(KeyCode.Escape))
            .Subscribe(_ => HandleEscapeKey())
            .AddTo(this);
    }

    public void Pause() { isPaused = true; }
    public void Resume() { isPaused = false; }

    

    void HandleEscapeKey()
    {
        if(isPaused)
        {
            Resume();
            UICamera.SetActive(false);
            UIPanel.SetActive(false);
            ChickenCounter.SetActive(true);
        }
        else
        {
            Pause();
            UICamera.SetActive(true); 
            UIPanel.SetActive(true);
            ChickenCounter.SetActive(false);

        }
    }

}
