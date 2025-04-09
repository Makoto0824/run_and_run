using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    public static PlayerSpawner instance;

    public GameObject[] playerPrefabs;//ref to player prefabs

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SpawnPlayer()
    {   //sapwn the selected player
        GameObject player = Instantiate(playerPrefabs[GameManager.instance.selectedSkin], transform.position,
            Quaternion.identity);
    }
}
