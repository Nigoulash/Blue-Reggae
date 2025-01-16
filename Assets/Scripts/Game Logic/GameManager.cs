using UnityEngine;

public class GameManager : MonoBehaviour
{
 // Static bool to track if the player is dead
    public static bool isDead = false;

    // This function is called when the player dies
    public void PlayerDies()
    {
        isDead = true;
        // Optionally, add more game over logic here like UI, stopping the game, etc.
        Debug.Log("Player is dead! Game Over!");
    }

    void Update()
    {
        // For testing: You can use this to pause the game when player dies
        if (isDead)
        {
            // Example: Freeze the game (pause)
            Time.timeScale = 0;
        }
    }
}
