
using UniRx;
using UnityEngine;


public class Orb : MonoBehaviour
{
    private OrbSpawn orbSpawn;
    private EventManager eventManager;

    private void Start()
    {
        eventManager = FindObjectOfType<EventManager>();
    }


    public void OrbCollected()
    {
        eventManager.TailGrowEvent();
        print("orb");
        orbSpawn = FindObjectOfType<OrbSpawn>();
        orbSpawn.orbCount.Value -= 1;
        DestroyImmediate(gameObject);
        
    }
}
