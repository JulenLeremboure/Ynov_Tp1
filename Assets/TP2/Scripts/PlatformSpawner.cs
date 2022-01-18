using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public List<GameObject> platformPrefabs;
    public List<GameObject> platformsInLevel;
    public GameObject platformParent;
    public int platformInLevelSize = 10;

    public float minPlatformXOffset = 5;
    public float maxPlatformXOffset = 10;
    public float minPlatformYOffset = -3.5f;
    public float maxPlatformYOffset = 2;

    [Range(1f,10f)]
    public RangeAttribute minMaxPlatformXOffset;

    [Range(-3.5f, 2f)]
    public RangeAttribute minMaxPlatformYOffset;

    private GameObject player;
    private Camera camera;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = FindObjectOfType<Camera>();
        platformsInLevel.Clear();

        platformsInLevel.Add(Instantiate(GetRandomPlatform(), platformParent.transform));

        for (int i = 1; i < platformInLevelSize; i++)
        {
            platformsInLevel.Add(Instantiate(GetRandomPlatform(), platformParent.transform));
            platformsInLevel[i].transform.position = new Vector2(platformsInLevel[i - 1].transform.position.x + GetRandomXOffset(), GetRandomYOffset());
        }

        platformsInLevel = platformsInLevel.OrderBy(r => r.transform.position.x).ToList();

        //Set up the begin of the parti of the dead
        Vector2 middlePlatformPosition = platformsInLevel[platformsInLevel.Count / 2 - 1].transform.position;
        player.transform.position = new Vector2(middlePlatformPosition.x, middlePlatformPosition.y + 2);
        camera.transform.position = new Vector3(middlePlatformPosition.x, camera.transform.position.y, camera.transform.position.z);
    }

    public void MovePlatform()
    {
        platformsInLevel.Remove(platformsInLevel[0]);
        Destroy(platformsInLevel[0]);

        platformsInLevel.Add(Instantiate(GetRandomPlatform(), platformParent.transform));

        platformsInLevel[platformsInLevel.Count-1].transform.position = new Vector2(
                platformsInLevel[platformsInLevel.Count - 2].transform.position.x + GetRandomXOffset(),
                GetRandomYOffset()
            );

        //platformsInLevel[platformInLevelSize - 1]
        //float xPosition = platformsInLevel[platformsInLevel.Count - 1].transform.position.x + GetRandomXOffset();
        //movedPlatform.transform.position = new Vector2(xPosition, GetRandomYOffset());
        //platformsInLevel.Add(movedPlatform);
    }

    private float GetRandomXOffset()
    {
        return Random.Range(minPlatformXOffset, maxPlatformXOffset);
    }

    private float GetRandomYOffset()
    {
        return Random.Range(minPlatformYOffset, maxPlatformYOffset);
    }

    private GameObject GetRandomPlatform()
    {
        return platformPrefabs[Random.Range(0, platformPrefabs.Count)];
    }
}
