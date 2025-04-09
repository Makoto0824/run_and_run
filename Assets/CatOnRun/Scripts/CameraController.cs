using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController instance;

    private float speed = 12; //speed of camera
    private Rigidbody2D myBody;//ref to rigibody component
    private Vector3 defaultPos;//start positon

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>(); //getting ref to rigidbody
        defaultPos = transform.position;//setting default pos
    }

	// Update is called once per frame
	void Update ()
    {
        myBody.linearVelocity = new Vector2(speed, 0);//changing velocity
	}
    //method used to get player speed
    public void GetPlayerSpeed(float playerSpeed)
    {
        speed = playerSpeed;
    }

    public void Reset() //method which resets the camera position
    {
        transform.position = defaultPos;
        //calls the method from player spawner
        PlayerSpawner.instance.SpawnPlayer();
    }

}
