using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;

    public void Pause() { isPaused = true; }
    public void Resume() { isPaused = false; }



}
