using UnityEngine;

namespace Controllers
{
    public class EnemyMovementsController : AbstractMovementsController
    {
        [SerializeField] private float speed;
        
        private Transform _playerTransform;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            
            if (_playerTransform != null) return;
            
            var player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                _playerTransform = player.transform;
            }
            else
            {
                Debug.LogWarning($"[{nameof(EnemyMovementsController)}] Player not found!");
            }
        }

        protected override void Move()
        {
            if (_playerTransform == null) return;

            FollowPlayer();
        }

        private void FollowPlayer()
        {
            var direction = (_playerTransform.position - transform.position);
            direction.y = 0f;
            
            //var distance = direction.magnitude;
            
            var targetRotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.fixedDeltaTime * speed
            );

            var movement = direction.normalized * speed;
            _rigidbody.linearVelocity = new Vector3(movement.x, _rigidbody.linearVelocity.y, movement.z);
        }
    }
}