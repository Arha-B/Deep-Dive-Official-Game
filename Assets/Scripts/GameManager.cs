using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game State")]
    public bool hasTreasure = false;
    public bool isGameOver = false;
    
    // Depth tracking (Optional, for UI)
    public float currentDepth; 

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    public void GrabTreasure()
    {
        hasTreasure = true;
        Debug.Log("TREASURE ACQUIRED! The Fih are becoming aggressive!");
        // Triggers global alarm or music change here
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        Debug.Log("Game Over: Blown up or Eaten.");
        // Show Game Over UI
        Time.timeScale = 0; 
    }

    public void WinGame()
    {
        if (isGameOver) return;
        isGameOver = true;
        Debug.Log("Surface Reached! You Win!");
        // Show Win UI
        Time.timeScale = 0; 
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}


