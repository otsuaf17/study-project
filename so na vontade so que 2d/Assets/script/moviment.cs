using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moviment : MonoBehaviour
{
    
    
    [Header("Dash variable")]
    /*----Dash-----*/

    //Dash control variables

    private bool canDash = true;
    public bool isDashing { get; set; }
    private float dashingPower = 20f;
    private float dashingTime = 0.1f;
    private float dashingCooldown = 0.1f;
    private const int VeloDash = 7;

    //Dash animation

    GhostDash _ghostDash;
    Rigidbody2D _rigidbody2D;
    [SerializeField]ParticleSystem _dust;

    [Header("Player variable")]
    /*--------Player variable base--------*/
    public float playerSpeed;
    public float MoveX { get; set; }

    /*-----------Parkour sistem -----------*/

    /*-------Wall Sliding---------*/
    [Header("Wall Slide / Wall Jump")]
    [SerializeField] private bool isWallSliding;
    private float wallSlidingSpeed = 10f;
    /*---------Wall jump----------*/
    public bool isWallJumping { get; set; }
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.1f;
    [SerializeField] private Vector2 wallJumpingPower = new Vector2(5f, 10f);
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    [Header("Jump")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public bool isJumping { get; set; }
    [SerializeField] private float Jump;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _ghostDash = FindObjectOfType<GhostDash>();

    }

    void Update()
    {
        CreateDust();
        MyInput();
        WallJump();
        WallSlide();
    }
    public void MyInput()
    {
        if (isDashing)
            return;
        //moveX
        MoveX = Input.GetAxisRaw("Horizontal") * playerSpeed * Time.deltaTime;

        //Dash
        if (Input.GetKeyDown(KeyCode.L) && canDash)
        {
            StartCoroutine(Dash());
        }

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Jump);
            isJumping = true;
            if (!isWallJumping)
                _dust.Play();
        }
        if (Input.GetKeyUp(KeyCode.Space) && _rigidbody2D.velocity.y > 0f)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * 0.3f);
            isJumping = false;
        }
    }
    private void FixedUpdate()
    {
        if (isDashing)
            return;
        if(!isWallJumping)
            _rigidbody2D.velocity = new Vector2(playerSpeed * MoveX, _rigidbody2D.velocity.y);
    }
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    public bool IsWalled()
    {
        return Physics2D.OverlapBox(wallCheck.position, new Vector2(0.03180504f, 1.5f), 0f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && MoveX != 0f)
        {
            isWallSliding = true;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Mathf.Clamp(_rigidbody2D.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            _rigidbody2D.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;
            if (transform.localScale.x != wallJumpingDirection)
            {

                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }
    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void CreateDust()
    {
        if (IsGrounded() && !isWallJumping)
        {
            if (Input.GetButtonDown("Horizontal"))
                _dust.Play();
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = _rigidbody2D.gravityScale;
        _rigidbody2D.gravityScale = 0f;
        _rigidbody2D.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        _ghostDash.MakeGhost = true;

        yield return new WaitForSeconds(dashingTime);
        _ghostDash.MakeGhost = false;
        _rigidbody2D.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
