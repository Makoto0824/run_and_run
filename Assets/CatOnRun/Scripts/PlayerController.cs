using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Rigidbody2D myBody;
    [SerializeField]
    private CapsuleCollider2D runningCol, slidingCol;

    private float speed = 12f;
    private float jumpForce = 13, fallLimit = -9;
    private float groundRadius = 0.1f, roofRadius = 0.5f;

    [SerializeField]
    private Transform ground, roof;
    [SerializeField]
    private LayerMask whatIsGround, whatIsRoof;
    [SerializeField]
    private Animator anim;

    private GameObject cameraObj;
    private float originalSpeed;
    private bool isStanding = true;
    private bool canJump, getDown = false;
    private bool isGrounded, roofAvai, slideBtnPressed;
    private AudioSource audioSource;

    [SerializeField]
    private AudioSource audioSource2;

    [HideInInspector]
    public bool isDead = false;
    public float recoverySpeed = 1.5f;

    [HideInInspector]
    public managerVars vars;

    private bool canMove = false;
    private bool isJumpSoundPlaying = false;

    void OnEnable()
    {
        vars = Resources.Load("managerVarsContainer") as managerVars;
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        anim.SetBool("IsStanding", isStanding);
        anim.SetBool("IsGrounded", isGrounded = true);

        myBody = GetComponent<Rigidbody2D>();
        var audioSources = GetComponents<AudioSource>();
        audioSource = audioSources[0];
        audioSource2 = audioSources[1];

        cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        CameraController.instance.GetPlayerSpeed(speed);
        originalSpeed = speed;
        slidingCol.enabled = false;

        canMove = false;
        myBody.linearVelocity = Vector2.zero;
    }

    void Update()
    {
        if (GuiManager.instance.tutorial && canMove)
        {
            GroundCheck();
            RoofCheck();
            DeathCheck();

#if UNITY_EDITOR
            KeyboardControls();
#elif UNITY_IOS || UNITY_ANDROID
            TouchControls();
#endif
            ControlSpeed();
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            myBody.linearVelocity = new Vector2(speed, myBody.linearVelocity.y);
            if (getDown && !isGrounded)
            {
                myBody.linearVelocity = new Vector2(myBody.linearVelocity.x, 0);
                myBody.linearVelocity = new Vector2(myBody.linearVelocity.x, -32);
            }
            else if (getDown && isGrounded)
            {
                getDown = false;
            }
        }
    }

    void Jump()
    {
        if (canJump && !isDead && canMove)
        {
            canJump = false;
            isGrounded = false;
            anim.SetBool("IsGrounded", isGrounded);
            myBody.linearVelocity = new Vector2(myBody.linearVelocity.x, 0);
            myBody.linearVelocity = new Vector2(myBody.linearVelocity.x, jumpForce);

            if (!isJumpSoundPlaying)
            {
                isJumpSoundPlaying = true;
                audioSource.PlayOneShot(audioSource.clip);
                StartCoroutine(ResetJumpSound());
            }
        }
    }

    IEnumerator ResetJumpSound()
    {
        yield return new WaitForSeconds(0.1f);
        isJumpSoundPlaying = false;
    }

    void Slide()
    {
        if (isGrounded)
        {
            isStanding = false;
            runningCol.enabled = false;
            slidingCol.enabled = true;
            slideBtnPressed = true;
            anim.SetBool("IsStanding", false);
        }
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(ground.transform.position, groundRadius, whatIsGround);
        anim.SetBool("IsGrounded", isGrounded);
        canJump = isGrounded;
    }

    void KeyboardControls()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Jump();
        }
        else if (!isGrounded)
        {
            myBody.linearVelocity = new Vector2(myBody.linearVelocity.x, myBody.linearVelocity.y - 32 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Slide();
        }
    }

    void TouchControls()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    Jump();
                }
                else if (touch.position.x > Screen.width / 2)
                {
                    Slide();
                }
            }
        }
        else if (!isGrounded)
        {
            myBody.linearVelocity = new Vector2(myBody.linearVelocity.x, myBody.linearVelocity.y - 32 * Time.deltaTime);
        }
    }

    void RoofCheck()
    {
        anim.SetBool("IsStanding", isStanding);
        roofAvai = Physics2D.OverlapCircle(roof.transform.position, roofRadius, whatIsRoof);

        if (slideBtnPressed)
        {
            if (!roofAvai)
            {
                isStanding = true;
                runningCol.enabled = true;
                slidingCol.enabled = false;
                slideBtnPressed = false;
                anim.SetBool("IsStanding", true);
            }
        }
    }

    void DeathCheck()
    {
        if (transform.position.y < fallLimit)
        {
            isDead = true;
            GuiManager.instance.GameOver();
        }
    }

    void ControlSpeed()
    {
        if (cameraObj.transform.position.x > transform.position.x + 5)
        {
            speed = originalSpeed * recoverySpeed;
        }
        else
        {
            speed = originalSpeed;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ground.transform.position, groundRadius);
        Gizmos.DrawWireSphere(roof.transform.position, roofRadius);
    }

    public void Reset()
    {
        isDead = false;
        isStanding = true;
        isGrounded = true;
        canJump = true;
        getDown = false;
        slideBtnPressed = false;
        roofAvai = false;

        anim.SetBool("IsStanding", isStanding);
        anim.SetBool("IsGrounded", isGrounded);

        runningCol.enabled = true;
        slidingCol.enabled = false;

        myBody.linearVelocity = new Vector2(0, 0);
        transform.position = new Vector2(-5, 0);
    }

    public void StartMoving()
    {
        canMove = true;
    }
}
