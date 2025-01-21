using UnityEngine;

public class DeathEvent : MonoBehaviour
{
    [SerializeField] GameObject background;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isDead)
        {
            background.SetActive(false);
            transform.position = GameManager.startPosition;
            GameManager.isDead = false;
        }
    }
}
