using UnityEngine;

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

    [SerializeField] Transform _groundChecker;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] Transform _leftWallChecker;
    [SerializeField] Transform _rightWallChecker;
    [SerializeField] LayerMask wallLayer;

    bool isOnRightWall = false;
    bool isOnLeftWall = false;

    Rigidbody2D rb;

    CapsuleCollider2D capColl;
    CircleCollider2D cirColl;

    float coyoteTime = 0.2f;
    float coyoteTimeCounter;

    float jumpBufferTime = 0.2f;
    float jumpBufferCounter;

    Vector2 startPosition;

    //bool canMove;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capColl = GetComponent<CapsuleCollider2D>();
        cirColl = GetComponent<CircleCollider2D>();
        startPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCapsule(_groundChecker.position, new Vector2(1f, 0.2f), CapsuleDirection2D.Horizontal, 0, groundLayer);

        isOnRightWall = Physics2D.OverlapCapsule(_rightWallChecker.position, new Vector2(1f, 0.2f), CapsuleDirection2D.Vertical, 0, wallLayer);
        isOnLeftWall = Physics2D.OverlapCapsule(_leftWallChecker.position, new Vector2(1f, 0.2f), CapsuleDirection2D.Vertical, 0, wallLayer);


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

        //if (canMove)

        BasicMovement();

        Jump();

        Slide();

        OutOfBounds();


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
            if (isGrounded)
            {
                coyoteTimeCounter = coyoteTime;
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                jumpBufferCounter = jumpBufferTime;
            }
            else
            {
                jumpBufferCounter -= Time.deltaTime;
            }


            if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
            {

                {
                    rb.linearVelocity = new Vector2(rb.linearVelocityX, _jumpStrength);

                    jumpBufferCounter = 0f;

                }

            }

            if (Input.GetKeyUp(KeyCode.UpArrow) && rb.linearVelocityX > 0f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY * 0.5f);

                coyoteTimeCounter = 0f;
            }
        }

        void WallJump()
        {
            if (isOnLeftWall && Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.linearVelocity = new Vector2(-_jumpStrength / 2, _jumpStrength / 2);
            }

            if (isOnRightWall && Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.linearVelocity = new Vector2(_jumpStrength / 2, _jumpStrength / 2);
            }
        }

        void OutOfBounds()
        {
            if (transform.position.y < -5f)
            {
                transform.position = startPosition;
            }
        }

        void Slide()
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                capColl.enabled = false;
                cirColl.enabled = true;
            }

            else
            {
                capColl.enabled = true;
                cirColl.enabled = false;
            }
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.tag == "Wall")
        //    {
        //        canMove = false;
        //    }
        //}

    }
}
