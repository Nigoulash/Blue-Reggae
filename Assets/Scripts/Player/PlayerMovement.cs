using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _origMoveSpeed = 5.0f;
    float realMoveSpeed;
    float doubleMoveSpeed;
    private float direction = 0f;

    float doubleJumpSpeed;

    float longJump = 0f;
    [SerializeField] float _origJumpSpeed = 15f;
    float jumpStrength;
    bool isGrounded = false;

    [SerializeField] GameObject _groundChecker;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] GameObject _wallChecker;
    [SerializeField] LayerMask wallLayer;

    bool isOnWall = false;

    Rigidbody2D rb;
    SpriteRenderer sr;
    [SerializeField] Animator animator;

    CapsuleCollider2D capColl;
    CircleCollider2D cirColl;
    BoxCollider2D boxColl;

    [SerializeField] LayerMask hookLayer;
    bool isGrabbing = false;

    [SerializeField] float ledgeJump = 20f;

    float coyoteTime = 0.2f;
    float coyoteTimeCounter;

    float jumpBufferTime = 0.2f;
    float jumpBufferCounter;

    float speed;

    float grav = 3f;

    float flipped = 1.5f;

    GameObject hookObject;
    GameObject sliderObject;

    DistanceJoint2D dj;


    //float flipX;

    //bool isJumping = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        capColl = GetComponent<CapsuleCollider2D>();
        cirColl = GetComponent<CircleCollider2D>();
        boxColl = GetComponent<BoxCollider2D>();

        GameManager.startPosition = this.transform.position;
        dj = GetComponent<DistanceJoint2D>();

        GameManager.canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(flipped, 1.5f, 1.5f);

        isGrounded = Physics2D.OverlapCapsule(_groundChecker.transform.position, new Vector2(1f, 0.2f), CapsuleDirection2D.Horizontal, 0, groundLayer);

        isOnWall = Physics2D.OverlapCapsule(_wallChecker.transform.position, new Vector2(1f, 2f), CapsuleDirection2D.Vertical, 0, wallLayer);


        if (Input.GetKey(KeyCode.LeftShift))
        {
            doubleMoveSpeed = _origMoveSpeed * 1.5f;
            realMoveSpeed = doubleMoveSpeed;

            doubleJumpSpeed = _origJumpSpeed * 1.5f;
            jumpStrength = doubleMoveSpeed;
        }

        else
        {
            realMoveSpeed = _origMoveSpeed;
            jumpStrength = _origJumpSpeed;
        }

        if (rb.linearVelocityY < 0)
        {
            longJump = 0f;
            grav = 3f;
            rb.gravityScale = grav * 1.7f;
            animator.SetBool("Fall", true);
        }
        else
        {
            rb.gravityScale = grav;
            animator.SetBool("Fall", false);
        }

        if (rb.linearVelocityY == 0)
        {
            animator.SetBool("Jump", false);
        }


        if (GameManager.canMove)
        {
            Flip();

            BasicMovement();

            if (!isGrabbing)
            {
                Jump();
            }

            Slide();

            OutOfBounds();

            WallJump();

            GrabLedge();

            JumpAnimator();

            ClimbUp();

            GrabSlider();
        }
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

        if (Input.GetKey(KeyCode.UpArrow) && longJump < 4f && !isOnWall)
        {
            longJump += (Time.deltaTime * 4);
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
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpStrength + longJump);

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

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetBool("Jump", true);
        }
        //else
        //{
        //    animator.SetBool("Jump", false);
        //}


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
        if (isOnWall && (!GameManager.grabbingLedge || !GameManager.grabbingSlider))
        {
            if (rb.linearVelocityX == realMoveSpeed * (flipped / Mathf.Abs(flipped)))
            {
                rb.linearVelocityX = 0f;
            }
        }

        if (isOnWall && Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow) && flipped > 0)
        {
            rb.linearVelocity = new Vector2(jumpStrength * (-flipped / Mathf.Abs(flipped)), jumpStrength);

        }

        if (isOnWall && Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow) && flipped < 0 )
        {
            rb.linearVelocity = new Vector2(jumpStrength * (-flipped / Mathf.Abs(flipped)), jumpStrength);
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
            if (!GameManager.isNearSlider)
            {
                capColl.enabled = false;
                cirColl.enabled = true;
                isOnWall = false;
            }

            if (!isGrounded)
            {
                grav += 1f;
            }
            else
            {
                animator.SetBool("Slide", true);
                rb.linearVelocityX = realMoveSpeed * (flipped / Mathf.Abs(flipped));
            }
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            capColl.enabled = true;
            cirColl.enabled = false;
            animator.SetBool("Slide", false);
        }
    }

    void Flip()
    {
        if (!isGrabbing)
        {

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                flipped = -1.5f;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                flipped = 1.5f;
            }
        }
    }

    void GrabLedge()
    {
        if (GameManager.isNearLedge && GameManager.grabbingLedge)
        {
            hookObject = GameObject.Find(GameManager.hook);

            if (hookObject != null)
            {
                dj.connectedBody = hookObject.GetComponent<Rigidbody2D>();
                dj.enabled = true;
                dj.distance = 0.1f;
                animator.SetBool("Hang", true);
                isGrabbing = true;
            }
        }
    }

    void ClimbUp()
    {
        if (isGrabbing)
        {

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                animator.SetBool("Hang", false);
                animator.SetBool("Climb", true);
                dj.enabled = false;
                rb.linearVelocity = new Vector2(rb.linearVelocityX, ledgeJump);
                isGrabbing = false;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                animator.SetBool("Hang", false);
                isGrabbing = false;
                dj.enabled = false;
            }

        }
    }

    void GrabSlider()
    {
        if (GameManager.isNearSlider && GameManager.grabbingSlider)
        {
            sliderObject = GameObject.Find(GameManager.slider);

            if (sliderObject != null)
            {
                capColl.enabled = false;
                cirColl.enabled = false;
                boxColl.enabled = true;
                animator.SetBool("Super", true);
                rb.linearVelocityX = realMoveSpeed * (flipped / Mathf.Abs(flipped) + 1f);
                StartCoroutine("SlideUnder");
            }
        }
    }


    IEnumerator SlideUnder()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("Super", false);
        capColl.enabled = true;
        boxColl.enabled = false;

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Wall")
    //    {
    //        canMove = false;
    //    }
    //}
    
}
