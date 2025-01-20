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

    [SerializeField] GameObject _groundChecker;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] GameObject _wallChecker;
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

    float speed;

    float grav = 1.2f;

    int flipped = 1;

    //float flipX;

    //bool isJumping = false;

    //bool canMove;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        capColl = GetComponent<CapsuleCollider2D>();
        cirColl = GetComponent<CircleCollider2D>();
        GameManager.startPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(flipped, 1, 1);

        isGrounded = Physics2D.OverlapCapsule(_groundChecker.transform.position, new Vector2(1f, 0.2f), CapsuleDirection2D.Horizontal, 0, groundLayer);

        isOnWall = Physics2D.OverlapCapsule(_wallChecker.transform.position, new Vector2(1f, 2f), CapsuleDirection2D.Vertical, 0, wallLayer);


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

        if (rb.linearVelocityY < 0)
        {
            grav = 1.2f;
            rb.gravityScale = grav * 1.5f;
        }
        else
        {
            rb.gravityScale = grav;
        }


        //if (canMove)

        Flip();

        BasicMovement();

        Jump();

        Slide();

        OutOfBounds();

        WallJump();

        GrabLedge();

        JumpAnimator();
    }

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

        if (Input.GetKey(KeyCode.UpArrow) && grav > 0.6f)
        {
            grav -= Time.deltaTime;
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

    void JumpAnimator() 
    { 

        if (animator.GetBool("OnGround") && Input.GetKeyDown(KeyCode.UpArrow))
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
    }

    void WallJump()
    {
        if (isOnWall)
        {
            if (rb.linearVelocityX == 0)
            {
                rb.linearVelocityY = -grav;
            }
        }

        if (isOnWall && Input.GetKey(KeyCode.UpArrow) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            rb.linearVelocity = new Vector2(_jumpStrength * -flipped, _jumpStrength);

        }
    }

    void OutOfBounds()
    {
        if (transform.position.y < -10f)
        {
            GameManager.isDead = true;
        }
    }

    void Slide()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            capColl.enabled = false;
            cirColl.enabled = true;
            isOnWall = false;

            if (!isGrounded)
            {
                grav += 1f;
            }
            else
            {
                animator.SetBool("Slide", true);
                rb.linearVelocityX = 10f * flipped;
            }
        }

        else
        {
            capColl.enabled = true;
            cirColl.enabled = false;
            animator.SetBool("Slide", false);
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
            flipped = -1;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            flipped = 1;
        }
    }

    void GrabLedge()
    {
        if (GameManager.isNearLedge && GameManager.grabbingLedge)
        {
            rb.mass = 1f;
            rb.AddForce(GameManager.hookDestination, ForceMode2D.Force);
            Debug.Log("moving towards ledge");
        }

        else
        {
            rb.mass = 0f;
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
