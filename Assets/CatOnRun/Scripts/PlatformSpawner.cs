using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    public static PlatformSpawner instance;

    [SerializeField]
    private float startSpawnPosition;
    [SerializeField]
    private float spawnYPos = -3;

    private int randomChoice;
    private float lastPosition;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        lastPosition = startSpawnPosition;
        for (int i = 0; i < 5; i++)
        {
            SpawnPlatform();
        }
    }

    public void SpawnPlatform()
    {
        randomChoice = Random.Range(1, 16);
        GameObject platform = null;
        float offset = 1.7f;

        switch (randomChoice)
        {
            case 1:
            case 2:
                platform = ObjectPooling.instance.GetLargeSpace();
                platform.transform.position = new Vector2(lastPosition + offset, spawnYPos);
                lastPosition = platform.transform.position.x + 8.45f;
                break;

            case 3:
            case 4:
                platform = ObjectPooling.instance.GetNormal();
                platform.transform.position = new Vector2(lastPosition + offset, spawnYPos);
                lastPosition = platform.transform.position.x + offset;
                break;

            case 5:
            case 6:
                platform = ObjectPooling.instance.GetSpace();
                platform.transform.position = new Vector2(lastPosition + offset, spawnYPos);
                lastPosition = platform.transform.position.x + 5.05f;
                break;

            case 7:
            case 8:
                platform = ObjectPooling.instance.GetRaised();
                platform.transform.position = new Vector2(lastPosition + offset, spawnYPos);
                lastPosition = platform.transform.position.x + offset;
                break;

            case 9:
            case 10:
                platform = ObjectPooling.instance.GetLeftRaised();
                platform.transform.position = new Vector2(lastPosition + offset, spawnYPos);
                lastPosition = platform.transform.position.x + offset;
                break;

            case 11:
            case 12:
                platform = ObjectPooling.instance.GetRightRaised();
                platform.transform.position = new Vector2(lastPosition + offset, spawnYPos);
                lastPosition = platform.transform.position.x + offset;
                break;

            case 13:
                platform = ObjectPooling.instance.GetTwoPieces();
                platform.transform.position = new Vector2(lastPosition + 3.4f, spawnYPos);
                lastPosition = platform.transform.position.x + 3.4f;
                break;

            case 14:
                platform = ObjectPooling.instance.GetFourPieces();
                platform.transform.position = new Vector2(lastPosition + 5.12f, spawnYPos);
                lastPosition = platform.transform.position.x + 5.12f;
                break;

            case 15:
                platform = ObjectPooling.instance.GetSpecialJump();
                platform.transform.position = new Vector2(lastPosition + 6.85f, spawnYPos);
                lastPosition = platform.transform.position.x + 6.85f;
                break;
        }

        platform.SetActive(true);
        platform.GetComponent<PlatformController>().BasicSettings();
    }

    public void Reset()
    {
        var platforms = FindObjectsOfType<PlatformController>();
        foreach (var platform in platforms)
        {
            if (platform.deactivateAtLimit)
                platform.gameObject.SetActive(false);
        }

        lastPosition = startSpawnPosition;
        for (int i = 0; i < 5; i++)
        {
            SpawnPlatform();
        }
    }
}
