using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    public float speed;

    private readonly float destroyLimit = -6.5f;

    // Update is called once per frame
    void Update()
    {
        ControlLimits();
        transform.Translate(Time.deltaTime * speed * Vector3.up);
    }

    private void ControlLimits()
    {
        if (transform.position.x <= destroyLimit)
        {
            Destroy(gameObject);
        }
    }
}
