using UnityEngine;
using System.Collections;

public class Target2D : MonoBehaviour
{
    public float health = 50f;
    public float clickDamage = 50f;

    [Tooltip("Time in seconds before the target changes color")]
    public float lifetime = 1f;

    [Tooltip("Color to change to when missed or timed out")]
    public Color missedColor = Color.black;

    [Tooltip("Delay in seconds before removing the target after color change")]
    public float removalDelay = 0.5f;

    private bool isDestroyed = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        Debug.Log("Target2D: Target initialized");
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Target2D: No SpriteRenderer found on this object!");
        }
        StartCoroutine(LifetimeTimer());
    }

    IEnumerator LifetimeTimer()
    {
        yield return new WaitForSeconds(lifetime);
        if (!isDestroyed)
        {
            TimeOut();
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDestroyed) return;

        health -= damage;
        Debug.Log($"Target2D: Target took {damage} damage. Remaining health: {health}");
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDestroyed) return;

        isDestroyed = true;
        Debug.Log("Target2D: Target destroyed!");
        GameManager.TargetDestroyed();
        Destroy(gameObject);
    }

    void TimeOut()
    {
        if (isDestroyed) return;

        isDestroyed = true;
        Debug.Log("Target2D: Target timed out");
        ChangeColor();
        GameManager.MissedTarget();
        StartCoroutine(RemoveAfterDelay());
    }

    public void RemoveTarget()
    {
        if (isDestroyed) return;

        isDestroyed = true;
        Debug.Log("Target2D: Target removed due to missed click");
        ChangeColor();
        GameManager.MissedTarget();
        StartCoroutine(RemoveAfterDelay());
    }

    void ChangeColor()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = missedColor;
        }
    }

    IEnumerator RemoveAfterDelay()
    {
        yield return new WaitForSeconds(removalDelay);
        Destroy(gameObject);
    }
}