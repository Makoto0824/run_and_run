using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    private float speed = 12;
    private Rigidbody2D myBody;
    private Vector3 defaultPos;
    private bool canMove = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        defaultPos = transform.position;
    }

    void Update()
    {
        if (canMove)
        {
            myBody.linearVelocity = new Vector2(speed, 0);
        }
        else
        {
            myBody.linearVelocity = Vector2.zero;
        }
    }

    public void GetPlayerSpeed(float playerSpeed)
    {
        speed = playerSpeed;
    }

    public void Reset()
    {
        transform.position = defaultPos;
        canMove = false;
        PlayerSpawner.instance.SpawnPlayer();
    }

    public void StartMoving()
    {
        canMove = true;
    }
}
