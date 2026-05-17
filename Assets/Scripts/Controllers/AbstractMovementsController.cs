using UnityEngine;

namespace Controllers
{
    public abstract class AbstractMovementsController : MonoBehaviour
    {
        private const float DestroyLimit = -6.5f;

        protected void FixedUpdate()
        {
            ControlLimits();
            Move();
        }

        protected abstract void Move();

        private void ControlLimits()
        {
            if (transform.position.x <= DestroyLimit)
            {
                Destroy(gameObject);
            }
        }
    }
}