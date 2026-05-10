using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private readonly float _playerRotationSpeed = 150f;
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
        if (!_gameManager.IsGamePaused())
        {
            GameObject projectile = _projectilesPoller.GetAvailableItemFromPool();
            projectile.SetActive(true);
            projectile.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otgObject = other.gameObject;
        if (otgObject.CompareTag("Target"))
        {
            Debug.Log("Player is heated by Target.");
            Destroy(otgObject);
            DecreaseLives();
        }
    }

    private void DecreaseLives()
    {
        var energyLeft = ServiceLocator.Get<EnergyHandler>().DecreaseEnergy();
        if (energyLeft > 0) return;
        if (_gameManager.IsGameActive())
        {
            _gameManager.GameOver();
            ServiceLocator.Get<ResultsHandler>().ApplyScore();
        }
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
