using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _origMoveSpeed = 5.0f;
    float realMoveSpeed;
    float doubleMoveSpeed;
    private float direction = 0f;

    [SerializeField] float _origJumpSpeed = 0.5f;
    float realJumpSpeed;
    float doubleJumpSpeed;
    [SerializeField] int _jumpStrength = 2;
    bool isGrounded = false;

    [SerializeField] Transform _groundChecker;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] Transform _wallChecker;
    [SerializeField] LayerMask wallLayer;

    bool isOnWall = false;

    Rigidbody2D rb;
    SpriteRenderer sr;
    public Animator animator;

    CapsuleCollider2D capColl;
    CircleCollider2D cirColl;

    float coyoteTime = 0.2f;
    float coyoteTimeCounter;

    float jumpBufferTime = 0.2f;
    float jumpBufferCounter;

    Vector2 startPosition;

    float speed;
    //bool isJumping = false;

    //bool canMove;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        capColl = GetComponent<CapsuleCollider2D>();
        cirColl = GetComponent<CircleCollider2D>();
        startPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {        
        isGrounded = Physics2D.OverlapCapsule(_groundChecker.position, new Vector2(1f, 0.2f), CapsuleDirection2D.Horizontal, 0, groundLayer);

        isOnWall = Physics2D.OverlapCapsule(_wallChecker.position, new Vector2(1f, 2f), CapsuleDirection2D.Vertical, 0, wallLayer);


        if (Input.GetKey(KeyCode.LeftShift))
        {
            doubleMoveSpeed = _origMoveSpeed * 1.5f;
            realMoveSpeed = doubleMoveSpeed;

            doubleJumpSpeed = _origJumpSpeed * 1.5f;
            realJumpSpeed = doubleMoveSpeed;
        }

        else
        {
            realMoveSpeed = _origMoveSpeed;
            realJumpSpeed = _origJumpSpeed;
        }


        //if (canMove)

        Flip();

        BasicMovement();

        Jump();

        Slide();

        OutOfBounds();

        //WallJump();

        void BasicMovement()
        {

            direction = Input.GetAxisRaw("Horizontal");
            rb.linearVelocity = new Vector2(direction * realMoveSpeed, rb.linearVelocityY);

            speed = System.Math.Abs(rb.linearVelocityX);
            animator.SetFloat("Run", speed);
            //if (speed > 0.1f)

            //{
            //    animator.SetFloat("Run", speed);
            //}

            //if (speed < 0.1f)
            //{
            //    animator.SetFloat("Run", speed);
            //}


        }

        void Jump()
        {
            if (isGrounded)
            {
                coyoteTimeCounter = coyoteTime;
                animator.SetBool("OnGround", true);
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

            if (Input.GetKey(KeyCode.UpArrow))
            {
                animator.SetBool("Jump", true);
            }

            else 
            {
                animator.SetBool("Jump", false);
            }

            if (isGrounded)
            {
                animator.SetBool("OnGround", true);
            }

            else
            {
                animator.SetBool("OnGround", false);
            }


            if (Input.GetKeyUp(KeyCode.UpArrow) && rb.linearVelocityX > 0f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY * 0.5f);

                coyoteTimeCounter = 0f;
            }
        }

        void WallJump()
        {
            if (isOnWall)
            {
                Debug.Log("On Wall");
                rb.AddForce(new Vector2(0, -1) * 5f);
            }

            if (isOnWall && Input.GetKeyDown(KeyCode.UpArrow))
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

        void OnCollisionEnter(Collision collision)
        {
            // Check if the player collided with the laser
            if (collision.gameObject.CompareTag("Laser"))
            {
                GameManager.isDead = true; // Trigger player death  
            }
        }

        void Flip()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                sr.flipX = true;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                sr.flipX = false;
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
