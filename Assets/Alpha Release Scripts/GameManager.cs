using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float levelTime = 120f; // 2 minutes

    void Update()
    {
        levelTime -= Time.deltaTime;
        if (levelTime <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        // Handle game over logic here
        Debug.Log("Game Over! Time's up!");
        // For example, reload the scene:
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
