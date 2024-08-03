// PlayerController2D.cs
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public Camera mainCamera;

    private Vector2 mousePosition;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        // Mouse position
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Rotation
        Vector2 lookDir = mousePosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}