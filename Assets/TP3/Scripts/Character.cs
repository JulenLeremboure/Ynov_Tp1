using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Character : MonoBehaviour
{
    [SerializeField] private float inputForce = 10;

    private bool canMove;

    private PlayerInput playerInput;
    private Rigidbody rb;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticallInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0f, verticallInput).normalized;

        if(direction.magnitude > 0.1f)
        {
            rb.AddForce(direction * inputForce * Time.deltaTime);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed)
            Debug.Log("Jump");
    }

    public void ResetVelocity()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
