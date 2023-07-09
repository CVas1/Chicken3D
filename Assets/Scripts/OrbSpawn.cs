using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityAtoms.Editor;
using UnityEngine;

public class OrbSpawn : MonoBehaviour
{
    private int orbCount = -1;
    [SerializeField] private GameObject[] orbSpawnArea;
    [SerializeField] private GameObject orbPrefab;
    public ReactiveProperty<int> observedValue = new ReactiveProperty<int>(-1);
    [SerializeField] private int minOrbCount = 5;

    private void Start()
    {
        
        observedValue.Subscribe(newValue =>
        {
            if (newValue < minOrbCount)
            {
                OrbSpawnOnArea();
            }
        });
        observedValue.Value += 0;
        Observable.Interval(TimeSpan.FromMilliseconds(100))
            .Subscribe(_ => observedValue.Value -= 1) ;

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
        observedValue.Value += 1;
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
        print(ramdomX);
        float ramdomZ = UnityEngine.Random.Range(z2, z1);
        Transform randomTransform = new GameObject().transform;
        randomTransform.position = new Vector3(ramdomX, 0f, ramdomZ);
        return randomTransform;
    }
}
