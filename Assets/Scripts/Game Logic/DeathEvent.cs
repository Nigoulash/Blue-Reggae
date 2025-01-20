using UnityEngine;

public class DeathEvent : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isDead)
        {
            transform.position = GameManager.startPosition;
            GameManager.isDead = false;
        }
    }
}
