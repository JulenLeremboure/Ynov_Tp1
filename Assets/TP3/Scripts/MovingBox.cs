using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBox : MonoBehaviour
{
    private float speed;
    public float Speed { get { return speed; } set { speed = value; } }

    private BoxManager boxManager;
    private Transform targetToMoveOn;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        boxManager = FindObjectOfType<BoxManager>();

        speed = boxManager.BoxSpeed;
        targetToMoveOn = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetToMoveOn.position, ref velocity, 0.3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //La boîte est-elle à gauche, à droite, en haut, en bas ?
            int xVelocity = 0;
            int zVelocity = 0;

            if (Mathf.Abs(collision.transform.position.x - transform.position.x) >
               Mathf.Abs(collision.transform.position.z - transform.position.z))
            {
                xVelocity = collision.transform.position.x < transform.position.x ? 1 : -1;
            }
            else
            {
                zVelocity = collision.transform.position.z < transform.position.z ? 1 : -1;
            }

            Vector3 newBoxPosition = new Vector3(transform.position.x + xVelocity,
                                                  transform.position.y,
                                                  transform.position.z + zVelocity);

            //Si la nouvelle posiiton n'est pas hors limites du terrain...
            if(!boxManager.IsPositionOutOfterrainBounds(newBoxPosition))
            {
                targetToMoveOn.position = newBoxPosition;
                boxManager.AddPlayerAction(gameObject, transform.position, collision.transform.position);
            }
            else
            {
                Debug.Log("Out of terrain move !!");
            }
        }
    }
}
