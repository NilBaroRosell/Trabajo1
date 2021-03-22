using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementMuseum : MonoBehaviour
{
    private InputManager input;
    private bool onPortal = false;
    [SerializeField] private float velocity = 2.0f;
    [SerializeField] private float maxVelocity = 30.0f;
    private Vector3 right;
    private Vector3 forward;
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
        right = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);
        forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        right.Normalize();
        forward.Normalize();
        if (input.GetLeft())
        {
            rb.AddForce(-right * velocity, ForceMode.Force);
        }
        if (input.GetRight())
        {
            rb.AddForce(right * velocity, ForceMode.Force);
        }
        if (input.GetForward())
        {
            Debug.Log("FORWARD");
            rb.AddForce(forward * velocity, ForceMode.Force);
        }
        if (input.GetBackward())
        {
            rb.AddForce(-forward * velocity, ForceMode.Force);
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
