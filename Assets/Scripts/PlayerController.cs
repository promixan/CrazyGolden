using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float livesByDefault = 5;
    [SerializeField] private float playerSpeed = 10f;
    private float lifeCounter;
    [SerializeField] private readonly float playerRotationSpeed = 150f;
    private const float upXBorder = 13.0f;
    private const float downXBorder = -5.0f;
    private const float zBorder = 16.2f;

    private GameManager gameManager;
    private ItemsPooler projectilesPoller;

    private InputAction moveAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        InputSystem.actions.FindAction("Attack").performed += OnAttack;

        if (lifeCounter == 0)
        {
            lifeCounter = livesByDefault;
        }
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        projectilesPoller = GameObject.Find("Hearts Pooler").GetComponent<ItemsPooler>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gameManager.IsGamePaused())
        {
            Vector2 move = moveAction.ReadValue<Vector2>();
            float verticalInput = move.y;
            float horizontalInput = move.x;

            MovePlayer(verticalInput);
            RotatePlayer(verticalInput, horizontalInput);
            ConstrainPlayerPosition();
        }
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (!gameManager.IsGamePaused())
        {
            GameObject projectile = projectilesPoller.GetAvailableItemFromPool();
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
            DecreaseLifes();
        }
    }

    private void DecreaseLifes()
    {
        lifeCounter--;
        if (lifeCounter <= 0)
        {
            Debug.Log("Lifes are ended.");
            Destroy(gameObject);
            if (gameManager.IsGameActive())
            {
                gameManager.GameOver();
            }
        }
        else
        {
            Debug.Log("You loose a life. Rest is: " + lifeCounter);
        }
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
            transform.Rotate(Vector3.up, Time.deltaTime * playerRotationSpeed * horizontalInput);
        }
        if (verticalInput < 0 && horizontalInput != 0)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * playerRotationSpeed * (-horizontalInput));
        }
    }

    private void ConstrainPlayerPosition()
    {
        if (transform.position.x < downXBorder)
        {
            transform.position = new(downXBorder, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > upXBorder)
        {
            transform.position = new(upXBorder, transform.position.y, transform.position.z);
        }

        if (transform.position.z > zBorder)
        {
            transform.position = new(transform.position.x, transform.position.y, zBorder);
        }
        else if (transform.position.z < -zBorder)
        {
            transform.position = new(transform.position.x, transform.position.y, -zBorder);
        }
    }

    void OnDestroy()
    {
        InputSystem.actions.FindAction("Attack").performed -= OnAttack;
    }
}
