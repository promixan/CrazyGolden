using System;
using Game;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private readonly float _playerRotationSpeed = 150f;
    [SerializeField] private Transform firePoint;
    private const float UpXBorder = 13.0f;
    private const float DownXBorder = -5.0f;
    private const float ZBorder = 16.2f;

    private GameManager _gameManager;
    private ItemsPooler _projectilesPoller;
    private Animation _walkAnimation;
    private InputAction _moveAction;

    private void Awake()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _projectilesPoller = GameObject.Find("Hearts Pooler").GetComponent<ItemsPooler>();
        _walkAnimation = GetComponentInChildren<Animation>();
    }

    private void Start()
    {
        InputSystem.actions.FindAction("Attack").performed += OnAttack;
        
        ServiceLocator.Get<EnergyHandler>().ResetEnergy();
        ServiceLocator.Get<ResultsHandler>().ResetScore();
        ServiceLocator.Get<TimerHandler>().StartTimer();
    }
    
    private void Update()
    {
        if (!_gameManager.IsGamePaused())
        {
            if (_moveAction.IsPressed() && !_walkAnimation.isPlaying)
            {
                _walkAnimation.Play();
            }
            else if (!_moveAction.IsPressed() && _walkAnimation.isPlaying)
            {
                _walkAnimation.Stop();
            }
        }
    }

    void FixedUpdate()
    {
        if (!_gameManager.IsGamePaused())
        {
            Vector2 move = _moveAction.ReadValue<Vector2>();
            float verticalInput = move.y;
            float horizontalInput = move.x;

            MovePlayer(verticalInput);
            RotatePlayer(verticalInput, horizontalInput);
            ConstrainPlayerPosition();
        }
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (_gameManager.IsGamePaused()) return;
        
        var projectile = _projectilesPoller.GetAvailableItemFromPool();
        if (projectile == null) return;

        projectile.transform.position = firePoint.position;
        projectile.transform.rotation = firePoint.rotation;
        projectile.SetActive(true);

        var controller = projectile.GetComponent<ProjectileController>();
        controller.UpdateDirection(firePoint.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otgObject = other.gameObject;
        if (otgObject.CompareTag("Enemy"))
        {
            Debug.Log("Player is heated by Enemy.");
            Destroy(otgObject);
            DecreaseLives();
        }
    }

    private void DecreaseLives()
    {
        var energyLeft = ServiceLocator.Get<EnergyHandler>().DecreaseEnergy();
        if (energyLeft > 0) return;
        _gameManager.GameOver();
        Destroy(gameObject);
    }

    private void MovePlayer(float verticalInput)
    {
        if (verticalInput != 0)
        {
            transform.Translate(Time.deltaTime * playerSpeed * verticalInput * Vector3.right);
        }
    }

    private void RotatePlayer(float verticalInput, float horizontalInput)
    {
        if (verticalInput >= 0 && horizontalInput != 0)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * _playerRotationSpeed * horizontalInput);
        }
        if (verticalInput < 0 && horizontalInput != 0)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * _playerRotationSpeed * (-horizontalInput));
        }
    }

    private void ConstrainPlayerPosition()
    {
        if (transform.position.x < DownXBorder)
        {
            transform.position = new(DownXBorder, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > UpXBorder)
        {
            transform.position = new(UpXBorder, transform.position.y, transform.position.z);
        }

        if (transform.position.z > ZBorder)
        {
            transform.position = new(transform.position.x, transform.position.y, ZBorder);
        }
        else if (transform.position.z < -ZBorder)
        {
            transform.position = new(transform.position.x, transform.position.y, -ZBorder);
        }
    }

    void OnDestroy()
    {
        InputSystem.actions.FindAction("Attack").performed -= OnAttack;
    }
}
