using UnityEngine;


public class Orb : MonoBehaviour
{
    private OrbSpawn orbSpawn;
    //private EventManager eventManager;

    private void Start()
    {
        //eventManager = FindObjectOfType<EventManager>();
    }


    public void OrbCollected()
    {
        gameObject.SetActive(false);
        orbSpawn = FindObjectOfType<OrbSpawn>();
        orbSpawn.orbCount.Value -= 1;
        Destroy(gameObject);

    }
}
