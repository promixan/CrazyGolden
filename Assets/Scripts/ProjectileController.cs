using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    private readonly float _deactivationXDistance = 15.5f;
    private Vector3 _direction;

    void FixedUpdate()
    {
        transform.Translate(Time.deltaTime * speed * _direction, Space.World);
        ControlLimits();
    }

    private void OnTriggerEnter(Collider other)
    {
        var otgObject = other.gameObject;
        if (otgObject.CompareTag("Target"))
        {
            Debug.Log("Target is heated by projectile");
            Destroy(otgObject);
            gameObject.SetActive(false);
            ServiceLocator.Get<ResultsHandler>().UpdateScore(1);
        }

        if (otgObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy is heated.");
            Destroy(otgObject);
            gameObject.SetActive(false);
            ServiceLocator.Get<ResultsHandler>().UpdateScore(10);
        }

        if (otgObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }

    private void ControlLimits()
    {
        if (transform.position.x >= _deactivationXDistance 
            || transform.position.x <= -_deactivationXDistance)
        {
            gameObject.SetActive(false);
        }
    }

    public void UpdateDirection(Vector3 dir)
    {
        _direction = dir.normalized;
        transform.rotation = Quaternion.Euler(-270f, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
