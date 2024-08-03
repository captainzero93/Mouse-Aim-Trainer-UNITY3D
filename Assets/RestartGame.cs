using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void ResetGame()
    {
        // Stop all running coroutines
        StopAllCoroutines();

        // Reset time scale in case it was changed
        Time.timeScale = 1f;

        // Destroy all existing targets
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject target in targets)
        {
            Destroy(target);
        }

        // Reset the GameManager
        GameManager.Instance.ResetGame();

        Debug.Log("Game Reset");
    }

}