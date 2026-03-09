using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool hasTreasure = false;
    public bool isGameOver  = false;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void GrabTreasure()
    {
        hasTreasure = true;
        Debug.Log("Treasure grabbed!");
    }

    public void WinGame()
    {
        Debug.Log("You win!");
    }

    public void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game over!");
    }
}