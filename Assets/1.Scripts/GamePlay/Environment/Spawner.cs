using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _pipePrefab;


    [Header("Spawn Settings")]
    [SerializeField, Range(0, 10)] private float _height = 0.4f;
    [SerializeField, Range(-10, 10)] private float _startingPosX = 1.5f;
    [SerializeField, Range(0, 10)] private float _spawnRate = 1.4f;


    [Header("Debug")]
    [SerializeField] private List<GameObject> _activePipes = new List<GameObject>();
    private bool _playerResumed;

    // ----- SYSTEM -----
    public static Spawner Instance;


    private void Awake()
    {
        Instance = this;

        GameManager.OnGameStateChanged += HandleGameStateChange;
        Transition.OnFadeTransitionFinished += () => ClearAllPipes();
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChange;
        Transition.OnFadeTransitionFinished -= () => ClearAllPipes();
    }

    private void HandleGameStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Playing: StartSpawning(); break;
            case GameState.Pause: _playerResumed = true; break;
            case GameState.Ending: _playerResumed = false; StopSpawning(); break;
        }
    }

    private void StartSpawning()
    {
        if(_playerResumed) return;

        StopAllCoroutines();
        StartCoroutine(SpawnPipesRoutine());
    }

    private void StopSpawning()
    {
        StopAllCoroutines();
    }

    private void ClearAllPipes()
    {
        foreach (var pipe in _activePipes)
            Destroy(pipe);

        _activePipes.Clear();
    }

    private IEnumerator SpawnPipesRoutine()
    {
        while (true)
        {
            SpawnPipe();
            yield return new WaitForSeconds(_spawnRate);
        }
    }

    private void SpawnPipe()
    {
        // 0.2f offset ensures pipes don't clip into the ground
        float minY = -_height + 0.2f;
        float maxY = _height;
        Vector3 spawnPos = new Vector3(_startingPosX, Random.Range(minY, maxY), 0);

        GameObject pipe = Instantiate(_pipePrefab, spawnPos, Quaternion.identity);
        _activePipes.Add(pipe);
    }
}