using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Rigidbody2D myBody;
    [SerializeField]
    private CapsuleCollider2D runningCol, slidingCol;

	//プレイヤー移動速度　デフォルト13f
	private float speed = 12f;

	//プレイヤーのジャンプ力 ゲームオーバーになる高さ デフォルト12,-8
    private float jumpForce = 13 , fallLimit = -9;

    //地面を検出する衝突者の半径　デフォルトgroundRadius0.1f roofRadius0.5f
	private float groundRadius = 0.1f , roofRadius = 0.5f;

	//コライダーを作成する位置からの位置を参照する
    [SerializeField]
    private Transform ground , roof;

	//地上オブジェクトがオンのレイヤーへの参照
    [SerializeField]
    private LayerMask whatIsGround, whatIsRoof;

	//アニメの参照
	[SerializeField]
    private Animator anim;                        

    private GameObject cameraObj; //ref to camera
    private float originalSpeed;
    private bool isStanding = true; //we use this to check to see if player is standing or not
    private bool canJump, getDown = false;          //to check few settings
    private bool isGrounded, roofAvai, slideBtnPressed;//ref to see if player is on ground and roof is on top
    private AudioSource audioSource;

    [SerializeField]
    private AudioSource audioSource2;

    [HideInInspector]
    public bool isDead = false;                   //check if player is dead or not
    //how fast the player recovers from being pushed back
    public float recoverySpeed = 1.5f;

    [HideInInspector]
    public managerVars vars;


    private GameObject tutorialobj;
    private GuiManager tutorialScript;



    void OnEnable()
    {
        vars = Resources.Load("managerVarsContainer") as managerVars;
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Use this for initialization
    void Start ()
    {      
		anim.SetBool("IsStanding", isStanding);
		anim.SetBool("IsGrounded", isGrounded = true);

        tutorialobj = GameObject.Find("Main Camera");
        tutorialScript = tutorialobj.GetComponent<GuiManager>();

        myBody = GetComponent<Rigidbody2D>();      //we get the rigidbody component	
        audioSource = GetComponent<AudioSource>();


        AudioSource[] audioSources = GetComponents<AudioSource>();
        audioSource = audioSources[0];
        audioSource2 = audioSources[1];

        cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        CameraController.instance.GetPlayerSpeed(speed);
        originalSpeed = speed;
        slidingCol.enabled = false; //we want sliding collider deactive
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (tutorialScript.tutorial == true)
		{
			//Checks
			GroundCheck();
			RoofCheck();
			DeathCheck();

			//Controls
#if UNITY_EDITOR
			KeyboardControls();
#elif UNITY_IOS || UNITY_ANDROID
        TouchControls();
#endif
			//Speed Control
			ControlSpeed();
		}
	}

    void FixedUpdate()
    {
        myBody.linearVelocity = new Vector2(speed, myBody.linearVelocity.y);
        if (getDown && !isGrounded)
        {
            //we make its y velocity negetive so it get back to ground quickly
            //we first make the y velocity zero
            myBody.linearVelocity = new Vector2(myBody.linearVelocity.x, 0);
            myBody.linearVelocity = new Vector2(myBody.linearVelocity.x, -32);
        }
        else if (getDown && isGrounded)
        {   //if getDonw is true and player is grounded we make getDown false
            getDown = false;
        }
    }

    //ジャンプ
	void Jump()
    {
		//check if player is alive ad can jump
		if (canJump && !isDead)
        {          
            canJump = false;    //we then set canjump and isgrounded false
            isGrounded = false;
            anim.SetBool("IsGrounded", isGrounded);
            //we first make the y velocity zero
            myBody.linearVelocity = new Vector2(myBody.linearVelocity.x, 0);

            //audioSource.PlayOneShot(vars.jumpSound);
            audioSource.PlayOneShot(audioSource.clip);

            //and then we add the jumpforce
            myBody.linearVelocity = new Vector2(myBody.linearVelocity.x, jumpForce);
        }
    }

    //スライディング
	void Slide()
    {
		if (isStanding)
        {
 
            //audioSource.PlayOneShot(vars.slideSound);
			
            audioSource2.PlayOneShot(audioSource2.clip);

            //set it false
			isStanding = false;
			//slide button is clicked
            slideBtnPressed = true;
            //change the animation
            anim.SetBool("IsStanding", isStanding);
			//deactivate running collider
            runningCol.enabled = false;
			//actiavte sliding collider
            slidingCol.enabled = true;
		}
	}

	//地面を確認   
    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(ground.transform.position, groundRadius, whatIsGround);
        anim.SetBool("IsGrounded", isGrounded);

        //ジャンプしてるかどう
        if (isGrounded == true)
        {
            canJump = true;
        }
        else if (isGrounded == false)
        {
            canJump = false;
        }
    }

    void KeyboardControls()
    {   //if w or up arrow is clicked
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Jump();//jump
        }
        else
        {   //if none of them is pressed and player is not grounded
            if (!isGrounded)//add negeative y velocity
                myBody.linearVelocity = new Vector2(myBody.linearVelocity.x, myBody.linearVelocity.y - 32 * Time.deltaTime);
        }
        //if s or down arrow is pressed
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Slide();//slide
        }
    }

    void TouchControls()
    {   //check for any touched on the screen
        if (Input.touchCount > 0)
        {   //then loop trough each touch
            foreach (Touch touch in Input.touches)
            {
                //if touch is on left half of screen we jump
                if (touch.position.x < Screen.width / 2)
                {
                    Jump();
                }
                //if touch is on right half of screen we slide
                else if (touch.position.x > Screen.width / 2)
                {
                    Slide();
                }
            }         
        }
        else //if no touch is on screen and player not grounded
        {
            if (!isGrounded) //add downward velocity
                myBody.linearVelocity = new Vector2(myBody.linearVelocity.x, myBody.linearVelocity.y - 32 * Time.deltaTime);
        }
    }

    //天井の確認
    void RoofCheck()
    {
        //here we play our sliding animation  
        anim.SetBool("IsStanding", isStanding);

        roofAvai = Physics2D.OverlapCircle(roof.transform.position, roofRadius, whatIsRoof);

        if (slideBtnPressed)
        {
            StartCoroutine(KeepSliding());
        }
        else if (slideBtnPressed == false && roofAvai == false)
        {
            isStanding = true;
            runningCol.enabled = true;
            slidingCol.enabled = false;
        }
         

    }

    void DeathCheck()
    {
        if (transform.position.y < fallLimit || (transform.position.x - cameraObj.transform.position.x) <= -10)
        {
            //taking screenshot
            //ShareScreenShot.instance.TakeScreenshot();
            GameManager.instance.gameOver = true;
            GuiManager.instance.GameOver();
            gameObject.SetActive(false);
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(ground.transform.position, new Vector3(0, 0, 1), groundRadius);

        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(roof.transform.position, new Vector3(0, 0, 1), roofRadius);
    }
#endif

    void ControlSpeed()
    {
        //if the player gets behind the camera too much, we add a bit of speed so he recovers.
        if (cameraObj.transform.position.x > transform.position.x + 1 && speed == originalSpeed)
        {
            //we add recovery speed to originalSpeed and make that the new speed
            speed = originalSpeed + recoverySpeed;
        }

        //if the player gets too far ahead he'll slow down instead of speed up, this would be rare or impossible, but here just to make sure.
        if (cameraObj.transform.position.x < transform.position.x - 1 && speed == originalSpeed)
        {
            speed = originalSpeed - recoverySpeed;
        }

        //if the player is back to the middle, we make the speed normal again.
        if (cameraObj.transform.position.x > transform.position.x - 1 && 
            cameraObj.transform.position.x < transform.position.x + 1 && speed != originalSpeed)
        {
            speed = originalSpeed;
        }
    }

    IEnumerator KeepSliding()
    {
        yield return new WaitForSeconds(0.25f);

        if (!roofAvai)
        {
            slideBtnPressed = false;
            isStanding = true;
            runningCol.enabled = true;
            slidingCol.enabled = false;
        }
    }

}
