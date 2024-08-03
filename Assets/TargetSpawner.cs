using UnityEngine;
using UnityEngine.UI;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
    public float spawnRate = 1f;
    public float minX = -5f, maxX = 5f;
    public float minY = -3f, maxY = 3f;
    public float uiSafeZone = 50f; // Pixels to keep clear around UI elements

    private float nextSpawnTime;
    private Camera mainCamera;
    private Canvas mainCanvas;

    void Start()
    {
        ResetSpawner();
    }

    public void ResetSpawner()
    {
        nextSpawnTime = Time.time + spawnRate;
        UpdateReferences();
    }

    void UpdateReferences()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No main camera found in the scene!");
        }
        mainCanvas = FindObjectOfType<Canvas>();
        if (mainCanvas == null)
        {
            Debug.LogError("No canvas found in the scene!");
        }
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnTarget();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnTarget()
    {
        if (mainCamera == null || mainCanvas == null)
        {
            UpdateReferences();
            if (mainCamera == null || mainCanvas == null)
            {
                Debug.LogError("Cannot spawn target: Camera or Canvas is missing.");
                return;
            }
        }

        Vector2 spawnPosition;
        int attempts = 0;
        const int maxAttempts = 50;

        do
        {
            spawnPosition = new Vector2(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY)
            );
            attempts++;
        } while (IsOverlappingUI(spawnPosition) && attempts < maxAttempts);

        if (attempts < maxAttempts)
        {
            GameObject spawnedTarget = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
            spawnedTarget.name = "Target";
            Debug.Log($"Target spawned at {spawnPosition}");
        }
        else
        {
            Debug.LogWarning("Failed to find a spawn position not overlapping UI");
        }
    }

    bool IsOverlappingUI(Vector2 worldPosition)
    {
        if (mainCamera == null || mainCanvas == null) return false;

        Vector2 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);

        // Check if the position is within the safe zone of any UI element
        Graphic[] uiElements = mainCanvas.GetComponentsInChildren<Graphic>();
        foreach (Graphic ui in uiElements)
        {
            if (ui == null) continue;

            RectTransform rectTransform = ui.rectTransform;
            Vector2 uiMin = rectTransform.TransformPoint(rectTransform.rect.min);
            Vector2 uiMax = rectTransform.TransformPoint(rectTransform.rect.max);

            // Expand the UI element's bounds by the safe zone
            uiMin -= new Vector2(uiSafeZone, uiSafeZone);
            uiMax += new Vector2(uiSafeZone, uiSafeZone);

            if (screenPosition.x >= uiMin.x && screenPosition.x <= uiMax.x &&
                screenPosition.y >= uiMin.y && screenPosition.y <= uiMax.y)
            {
                return true; // Overlapping UI
            }
        }

        return false; // Not overlapping any UI
    }
}