using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject terrain;
    public GameObject WinUI;

    public float terrainMoveSpeed = 5f;

    public float maxRotationHorizontal = .3f;
    public float maxRotationVertical = .3f;

    private void Start()
    {
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        float rotationInputHorizontal = Input.GetAxis("Horizontal");
        float rotationInputVertical = Input.GetAxis("Vertical");

        terrain.transform.Rotate(Vector3.right * rotationInputVertical * terrainMoveSpeed * Time.deltaTime);
        terrain.transform.Rotate(Vector3.back * rotationInputHorizontal * terrainMoveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Trap"))
        {
            ResetGame();
        }

        if (collision.collider.CompareTag("Win"))
        {
            WinUI.SetActive(true);
        }
    }

    private void ResetGame()
    {
        transform.position = spawnPoint.transform.position;
        WinUI.SetActive(false);
    }
}
