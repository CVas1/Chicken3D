using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public UnityEvent TailGrow;

    public void TailGrowEvent()
    {
        TailGrow.Invoke();
    }
}