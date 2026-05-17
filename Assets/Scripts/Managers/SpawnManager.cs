using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager _gameManager;
    public GameObject playerPrefab;
    public GameObject[] goodAnimalPrefabs;
    public GameObject[] enemyPrefabs;

    private readonly Vector3 playerDefaultPosition = new(-3.5f, 0.5f, 0f);
    private readonly float zPositionRange = 15.0f;
    public Collider[] maxTargetColliders;

    private readonly float _minTargetSpawnTimeOutRange = 1.0f;
    private readonly float _maxTargetSpawnTimeOutRange = 2.0f;
    private readonly float _minEnemySpawnTimeOutRange = 2.0f;
    private readonly float _maxEnemySpawnTimeOutRange = 3.0f;

    private Coroutine _spawnTargetCoroutine;
    private Coroutine _spawnEnemyCoroutine;

    private void Awake()
    {
        maxTargetColliders = new Collider[5];
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void StartNewGame()
    {
        ResetGame();
        SpawnPlayer();
        _spawnTargetCoroutine = StartCoroutine(SpawnTargetRoutine());
        _spawnEnemyCoroutine = StartCoroutine(SpawnEnemyRoutine());
    }

    public void ResetGame()
    {
        if (_spawnTargetCoroutine != null)
        {
            StopCoroutine(_spawnTargetCoroutine);
            StopCoroutine(_spawnEnemyCoroutine);
        }
        DestroyAllObjects(GameObject.FindGameObjectsWithTag("Player"));
        DestroyAllObjects(GameObject.FindGameObjectsWithTag("Target"));
        DestroyAllObjects(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    private void SpawnPlayer()
    {
        Instantiate(playerPrefab, playerDefaultPosition, playerPrefab.transform.rotation);
    }
    
    IEnumerator SpawnTargetRoutine()
    {
        while (_gameManager.IsGameActive())
        {
            float timeToWait = Random.Range(_minTargetSpawnTimeOutRange, _maxTargetSpawnTimeOutRange);
            yield return new WaitForSeconds(timeToWait);
            if (_gameManager.IsGameActive())
            {
                SpawnTarget();
            }
        }
    }
    
    IEnumerator SpawnEnemyRoutine()
    {
        while (_gameManager.IsGameActive())
        {
            var timeToWait = Random.Range(_minEnemySpawnTimeOutRange, _maxEnemySpawnTimeOutRange);
            yield return new WaitForSeconds(timeToWait);
            if (_gameManager.IsGameActive())
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnTarget()
    {
        var target = goodAnimalPrefabs[Random.Range(0, goodAnimalPrefabs.Length)];
        Spawn(target);
    }
    
    private void SpawnEnemy()
    {
        var target = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Spawn(target);
    }

    private void Spawn(GameObject target)
    {
        var position = GenerateRandomPosition();
        Instantiate(target, position, target.transform.rotation);
    }

    private Vector3 GenerateRandomPosition()
    {
        float zPosition = Random.Range(-zPositionRange, zPositionRange);
        Vector3 potentialPosition = new(17.0f, 0.28f, zPosition);
        /*int colliderNumber = Physics.OverlapSphereNonAlloc(potentialPosition, 1.5f, maxTargetColliders);
        if (colliderNumber > 0 && IsTargetExists())
        {
            return GenerateRandomPosition();
        }*/
        return potentialPosition;
    }

    private bool IsTargetExists()
    {
        for (int i = 0; i < maxTargetColliders.Length; i++)
        {
            Collider c = maxTargetColliders[i];
            if (c != null && c.CompareTag("Target"))
            {
                return true;
            }
        }
        return false;
    }

    private void DestroyAllObjects(GameObject[] objects)
    {
        foreach (GameObject o in objects)
        {
            Destroy(o);
        }
    }
}
