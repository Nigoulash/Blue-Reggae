using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BasicMovement();
        
    }


    void BasicMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            {
                transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            {
                transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
            }
        }
    }
}
