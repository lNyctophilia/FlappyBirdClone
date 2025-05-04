using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;
    public GameObject Pipe;
    [SerializeField, Range(0, 10)] float Height;
    [SerializeField, Range(-10, 10)] float startPos;
    [SerializeField, Range(0, 10)] float spawnRate;
    List<GameObject> PipeList = new List<GameObject>();
    private void Awake()
    {
        Instance = this;
    }
    public void StartSpawn()
    {
        InvokeRepeating("SpawnPipe", 0, spawnRate);
    }
    public void StopSpawn()
    {
        CancelInvoke("SpawnPipe");
    }
    public void DestroyPipe()
    {
        for (int i = 0; i < PipeList.Count; i++)
        {
            Destroy(PipeList[i]);
        }
        PipeList.Clear();
    }
    void SpawnPipe()
    {
        PipeList.Add(Instantiate(Pipe, new Vector3(startPos, Random.Range(-Height + 0.2f, Height), 0), Quaternion.identity));
    }
}
