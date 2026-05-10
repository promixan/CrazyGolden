using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FixedFOV : MonoBehaviour
{
    public float targetHorizontalFOV = 50f;

    private void Update()
    {
        var cam = GetComponent<Camera>();

        var aspect = (float)Screen.width / Screen.height;

        var verticalFOV =
            2f
            * Mathf.Atan(Mathf.Tan(targetHorizontalFOV * Mathf.Deg2Rad / 2f) / aspect)
            * Mathf.Rad2Deg;

        cam.fieldOfView = verticalFOV;
    }
}
