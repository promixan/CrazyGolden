using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject playerPrefab;
    public GameObject[] targetPrefabs;

    private readonly Vector3 playerDefaultPosition = new(-3.5f, 0.5f, 0f);
    private readonly float zPositionRange = 15.0f;
    public Collider[] maxTargetColliders;

    private readonly float minTargetSpawnTimeOutRange = 2.0f;
    private readonly float maxTargetSpawnTimeOutRange = 3.0f;

    private Coroutine spawnCoroutine;

    void Awake()
    {
        maxTargetColliders = new Collider[5];
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void StartNewGame()
    {
        ResetGame();
        SpawnPlayer();
        spawnCoroutine = StartCoroutine(SpawnTargetRoutine());
    }

    public void ResetGame()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        DestroyAllObjects(GameObject.FindGameObjectsWithTag("Player"));
        DestroyAllObjects(GameObject.FindGameObjectsWithTag("Target"));
    }

    private void SpawnPlayer()
    {
        Instantiate(playerPrefab, playerDefaultPosition, playerPrefab.transform.rotation);
    }
    
    IEnumerator SpawnTargetRoutine()
    {
        while (gameManager.IsGameActive())
        {
            float timeToWait = Random.Range(minTargetSpawnTimeOutRange, maxTargetSpawnTimeOutRange);
            yield return new WaitForSeconds(timeToWait);
            if (gameManager.IsGameActive())
            {
                SpawnTarget();
            }
        }
    }

    private void SpawnTarget()
    {
        Vector3 position = GenerateRandomPosition();
        GameObject target = targetPrefabs[Random.Range(0, targetPrefabs.Length)];
        Instantiate(target, position, target.transform.rotation);
    }

    private Vector3 GenerateRandomPosition()
    {
        float zPosition = Random.Range(-zPositionRange, zPositionRange);
        Vector3 potentialPosition = new(17.0f, 0.28f, zPosition);
        int colliderNumber = Physics.OverlapSphereNonAlloc(potentialPosition, 1.5f, maxTargetColliders);
        if (colliderNumber > 0 && IsTargetExists())
        {
            return GenerateRandomPosition();
        }
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
