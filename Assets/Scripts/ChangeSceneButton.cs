using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        print("hi");
        LevelManager.Instance.LoadScene(sceneName);
    }
}