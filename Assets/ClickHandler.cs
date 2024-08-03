using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Left mouse button
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                Target2D target = hit.collider.GetComponent<Target2D>();
                if (target != null)
                {
                    // Hit a target
                    target.TakeDamage(target.clickDamage);
                }
                else
                {
                    // Hit something, but not a target
                    MissedClick();
                }
            }
            else
            {
                // Clicked but didn't hit anything
                MissedClick();
            }
        }
    }

    void MissedClick()
    {
        GameManager.MissedTarget();
        Target2D[] allTargets = FindObjectsOfType<Target2D>();
        foreach (Target2D target in allTargets)
        {
            target.RemoveTarget();  // This is correct, matching the method in Target2D
        }
    }
}