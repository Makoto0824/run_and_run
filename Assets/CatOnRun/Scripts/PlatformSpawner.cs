using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour {

    public static PlatformSpawner instance;

    //this is the starting position we start spawning platform after the first platform that exists in the scene.
    [SerializeField]
    private float startSpawnPosition;

    //this is the y position that all platforms will be spawned
    [SerializeField]
    private float spawnYPos = -3;

    //this random number determine what platform will be spawned
    private int randomChoice;

    //this is to store last platform position
    private float lastPosition;
    private PlatformController[] platforms; //ref to all plafroms in the scene

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

	// Use this for initialization
	void Start ()
    {
        lastPosition = startSpawnPosition;//set the last postion
        for (int i = 0; i < 5; i++)//sapwn 5 platforms at start
        {
            SpawnPlatform();
        }
	}
    //method which spawns the platforms
    public void SpawnPlatform()
    {
        //we choose the random number that will determine what platform will be spawned.
        randomChoice = Random.Range(1, 16);
        GameObject platform = null;
        if (randomChoice >= 1 && randomChoice <= 2) //LargeSpace
        {
            platform = ObjectPooling.instance.GetLargeSpace();
            platform.transform.position = new Vector2(lastPosition + 1.7f, spawnYPos);
            lastPosition = platform.transform.position.x + 8.45f;
        }

        if (randomChoice >= 3 && randomChoice <= 4)//Normal
        {
            platform = ObjectPooling.instance.GetNormal();
            platform.transform.position = new Vector2(lastPosition + 1.7f, spawnYPos);
            lastPosition = platform.transform.position.x + 1.7f;
        }

        if (randomChoice >= 5 && randomChoice <= 6)//Space
        {
            platform = ObjectPooling.instance.GetSpace();
            platform.transform.position = new Vector2(lastPosition + 1.7f, spawnYPos);
            lastPosition = platform.transform.position.x + 5.05f;
        }

        if (randomChoice >= 7 && randomChoice <= 8)//Raised
        {
            platform = ObjectPooling.instance.GetRaised();
            platform.transform.position = new Vector2(lastPosition + 1.7f, spawnYPos);
            lastPosition = platform.transform.position.x + 1.7f;
        }

        if (randomChoice >= 9 && randomChoice <= 10)//Raised Left
        {
            platform = ObjectPooling.instance.GetLeftRaised();
            platform.transform.position = new Vector2(lastPosition + 1.7f, spawnYPos);
            lastPosition = platform.transform.position.x + 1.7f;
        }

        if (randomChoice >= 11 && randomChoice <= 12)//Raised Right
        {
            platform = ObjectPooling.instance.GetRightRaised();
            platform.transform.position = new Vector2(lastPosition + 1.7f, spawnYPos);
            lastPosition = platform.transform.position.x + 1.7f;
        }

        if (randomChoice == 13)//TwoPieces
        {
            platform = ObjectPooling.instance.GetTwoPieces();
            platform.transform.position = new Vector2(lastPosition + 3.4f, spawnYPos);
            lastPosition = platform.transform.position.x + 3.4f;
        }

        if (randomChoice == 14)//FourPieces
        {
            platform = ObjectPooling.instance.GetFourPieces();
            platform.transform.position = new Vector2(lastPosition + 5.12f, spawnYPos);
            lastPosition = platform.transform.position.x + 5.12f;
        }

        if (randomChoice == 15)//SpecialJump
        {
            platform = ObjectPooling.instance.GetSpecialJump();
            platform.transform.position = new Vector2(lastPosition + 6.85f, spawnYPos);
            lastPosition = platform.transform.position.x + 6.85f;
        }
        platform.SetActive(true);
        platform.GetComponent<PlatformController>().BasicSettings();
    }

    //method which reset
    public void Reset()
    {   //get all the platforms in the scene
        platforms = FindObjectsOfType<PlatformController>();
        //then loop through them
        for (int i = 0; i < platforms.Length; i++)
        {   //check from deactivateAtLimit is true
            if (platforms[i].deactivateAtLimit == true)
                platforms[i].gameObject.SetActive(false);//deactiavte the platform
        }
        //reset lastpostion
        lastPosition = startSpawnPosition;
        for (int i = 0; i < 5; i++)//spawn 5 platfroms
        {
            SpawnPlatform();
        }
    }
}
