using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    private readonly float deactivationDistanse = 15.5f;

    void FixedUpdate()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.up);
        ControlLimits();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otgObject = other.gameObject;
        if (otgObject.CompareTag("Target"))
        {
            Debug.Log("Target is heated by projectile");
            Destroy(otgObject);
            this.gameObject.SetActive(false);
        }
    }

    private void ControlLimits()
    {
        if (transform.position.x >= deactivationDistanse)
        {
            this.gameObject.SetActive(false);
        }
    }
}
