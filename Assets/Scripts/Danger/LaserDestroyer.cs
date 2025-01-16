using System.Collections.Generic;
using UnityEngine;

public class LaserDestroyer : MonoBehaviour
{
    public float timeTilDestroy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Destroy(gameObject, timeTilDestroy); 
    }

        // This will handle the collision detection with the player
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collided with the laser
        if (collision.gameObject.CompareTag("Player"))
        {
            // Call the GameManager to handle the player’s death
            GameManager gameManager = FindObjectOfType<GameManager>(); // Find the GameManager in the scene
            if (gameManager != null)
            {
                gameManager.PlayerDies(); // Trigger player death
            }
        }
    }
}
