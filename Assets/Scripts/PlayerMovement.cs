using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _origMoveSpeed = 5.0f;
    float realMoveSpeed;
    float halfMoveSpeed;
    [SerializeField] float _origJumpSpeed = 0.5f;
    float realJumpSpeed;
    float halfJumpSpeed;
    [SerializeField] int _jumpStrength = 2;
    bool isGrounded = false;
    [SerializeField] GameObject _groundChecker;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            halfMoveSpeed = _origMoveSpeed / 2;
            realMoveSpeed = halfMoveSpeed;

            halfJumpSpeed = _origJumpSpeed / 2;
            realJumpSpeed = halfMoveSpeed;
        }

        else
        {
            realMoveSpeed = _origMoveSpeed;
            realJumpSpeed = _origJumpSpeed;
        }

        BasicMovement();

        Jump();
        
    }


    void BasicMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            {
                transform.Translate(Vector2.right * realMoveSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            {
                transform.Translate(Vector2.left * realMoveSpeed * Time.deltaTime);
            }
        }
    }


    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Physics2D.OverlapCircle(_groundChecker.transform.position, 0.1f, 3))
            {
                {
                    float yPos = transform.position.y + _jumpStrength;
                    Vector2 targetPosition = new Vector2(transform.position.x, yPos);
                    transform.position = Vector2.Lerp(transform.position, targetPosition, realJumpSpeed);
                }
            }
        }
    }
}
