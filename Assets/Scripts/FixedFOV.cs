using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FixedFOV : MonoBehaviour
{
    public float targetHorizontalFOV = 50f; // желаемый горизонтальный угол

    void Update()
    {
        Camera cam = GetComponent<Camera>();

        float aspect = (float)Screen.width / Screen.height;

        float verticalFOV = 2f * Mathf.Atan(Mathf.Tan(targetHorizontalFOV * Mathf.Deg2Rad / 2f) / aspect) * Mathf.Rad2Deg;

        cam.fieldOfView = verticalFOV;
    }
}
