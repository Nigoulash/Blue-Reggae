using UnityEngine;

public class CheckpointPassed : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < player.position.x)
        {
            animator.SetBool("Passed", true);
            GameManager.startPosition = new Vector2(transform.position.x + 3f, transform.position.y + 5f);
        }

        else
        {
            animator.SetBool("Passed", false);
        }
    }
}
