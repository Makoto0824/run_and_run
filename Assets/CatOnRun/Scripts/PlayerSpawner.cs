using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner instance;
    public GameObject[] playerPrefabs;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SpawnPlayer()
    {
        GameObject player = Instantiate(playerPrefabs[GameManager.instance.selectedSkin], transform.position, Quaternion.identity);
    }
}
