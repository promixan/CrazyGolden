using UnityEngine;

namespace Controllers
{
    public class TargetMovementsController : AbstractMovementsController
    {
        [SerializeField] private float speed;

        protected override void Move()
        {
            transform.Translate(Time.deltaTime * speed * Vector3.left, Space.World);
        }
    }
}