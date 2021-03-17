using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidMovement : MonoBehaviour
{
    private InputManager input;
    private Rigidbody rb;
    [SerializeField] private float velocity = 2.0f;
    [SerializeField] private float jumpForce = 10.0f;
    private bool isJumping = false;

    private void Awake()
    {
        input = gameObject.GetComponent<InputManager>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(input.GetLeft())
        {
            rb.AddForce(Vector3.right * velocity, ForceMode.Force);
        }
        if(input.GetRight())
        {
            rb.AddForce(-Vector3.right * velocity, ForceMode.Force);
        }
        if(input.GetJump())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Pyramid")
        {
            isJumping = false;
        }
    }
}
