using System.Collections;
using UnityEngine;

namespace Managers
{
    public class PowerUpsManager : MonoBehaviour
    {
        private GameManager _gameManager;
        private ItemsPooler _powerUpPooler;
        [SerializeField] private int minSpawnX = 1;
        [SerializeField] private int maxSpawnX = 12;
        [SerializeField] private int minSpawnZ = -15;
        [SerializeField] private int maxSpawnZ = 15;
        [SerializeField] private int timeToSpawn = 15;
        
        private Coroutine _spawnRoutine;

        private void Awake()
        {
            _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            _powerUpPooler = GetComponent<ItemsPooler>();
        }

        public void RunPowerUpSpawn()
        {
            _spawnRoutine = StartCoroutine(SpawnPowerUpRoutine());
        }

        public void StopPowerUpSpawn()
        {
            if (_spawnRoutine != null) StopCoroutine(_spawnRoutine);
            _powerUpPooler.DeactivateAllItems();
        }

        private IEnumerator SpawnPowerUpRoutine()
        {
            while (_gameManager.IsGameActive())
            {
                yield return new WaitForSeconds(timeToSpawn);
                if (!_gameManager.IsGameActive()) continue;
                var item = _powerUpPooler.GetAvailableItemFromPool();
                if (item == null) continue;
                var x = Random.Range(minSpawnX, maxSpawnX);
                var z = Random.Range(minSpawnZ, maxSpawnZ);
                var y = item.transform.position.y;
                var position = new Vector3(x, y, z);
                item.transform.position = position;
                item.SetActive(true);
            }
        }
    }
}