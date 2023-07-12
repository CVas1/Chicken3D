using UniRx;
using UnityEngine;

public class OrbSpawn : MonoBehaviour
{
    //private int orbCount = -1;
    [SerializeField] private GameObject[] orbSpawnArea;
    [SerializeField] private GameObject orbPrefab;
    public ReactiveProperty<int> orbCount = new ReactiveProperty<int>(0);
    [SerializeField] private int minOrbCount = 5;

    private void Start()
    {
        for(int i = 0; i < minOrbCount; i++) { OrbSpawnOnArea(); }
        orbCount.Subscribe(newValue =>
        {
            if (newValue < minOrbCount)
            {
                OrbSpawnOnArea();
            }
        });
       // Observable.Interval(TimeSpan.FromSeconds(2)).Subscribe(_ => observedValue.Value -= 1) ;

    }

    private void OrbSpawnOnArea()
    {
        Transform spawnPoint = GetRandomTransformBetween(orbSpawnArea[0].transform, orbSpawnArea[1].transform);
        while (CheckCollision(spawnPoint.position)) 
        {
            Destroy(spawnPoint.gameObject);
            spawnPoint = GetRandomTransformBetween(orbSpawnArea[0].transform, orbSpawnArea[1].transform);
        }
        GameObject go = Instantiate(orbPrefab, spawnPoint.position, Quaternion.identity);
        Destroy(spawnPoint.gameObject);
        orbCount.Value += 1;
    }

    private bool CheckCollision(Vector3 position)
    {
        return Physics.CheckSphere(position, 1.5f, LayerMask.GetMask("Objects"));
    }

    private Transform GetRandomTransformBetween(Transform startTransform, Transform endTransform)
    {
        float x1 = startTransform.position.x;
        float z1 = startTransform.position.z;
        float x2 = endTransform.position.x;
        float z2 = endTransform.position.z;

        float ramdomX = UnityEngine.Random.Range(x1, x2);
        float ramdomZ = UnityEngine.Random.Range(z2, z1);
        Transform randomTransform = new GameObject().transform;
        randomTransform.position = new Vector3(ramdomX, 0f, ramdomZ);
        return randomTransform;
    }


    
}
