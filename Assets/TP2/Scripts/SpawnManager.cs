using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    PlatformSpawner platformSpawner;
    // Start is called before the first frame update
    void Start()
    {
        platformSpawner = GetComponent<PlatformSpawner>();
    }

    public void SpawnPlatformWhenTriggerEntered()
    {
        platformSpawner.MovePlatform();
    }
}
