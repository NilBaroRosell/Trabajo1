using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementMuseum : MonoBehaviour
{
    private InputManager input;
    private bool onPortal = false;
    [SerializeField] private float velocity = 2.0f;
    [SerializeField] private float maxVelocity = 30.0f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        input = gameObject.GetComponent<InputManager>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (input.GetLeft())
        {
            rb.AddForce(-Vector3.right * velocity, ForceMode.Force);
        }
        if (input.GetRight())
        {
            rb.AddForce(Vector3.right * velocity, ForceMode.Force);
        }
        if (input.GetForward())
        {
            rb.AddForce(Vector3.forward * velocity, ForceMode.Force);
        }
        if (input.GetBackward())
        {
            rb.AddForce(-Vector3.forward * velocity, ForceMode.Force);
        }
        if (rb.velocity.magnitude > maxVelocity) rb.velocity = rb.velocity.normalized * maxVelocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Portal")
        {
            onPortal = true;
        }
    }

    public bool GetOnPortal()
    {
        return onPortal;
    }
}
