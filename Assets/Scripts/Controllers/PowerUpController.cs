using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class PowerUpController : MonoBehaviour
    {
        [SerializeField] private int secondsTillDespawn = 3;

        private void OnEnable()
        {
            StartCoroutine(DespawnRoutine());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
        
        private IEnumerator DespawnRoutine()
        {
            yield return new WaitForSeconds(secondsTillDespawn);
            gameObject.SetActive(false);
        }
    }
}
