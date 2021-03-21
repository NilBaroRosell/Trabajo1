using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool left;
    private bool right;
    private bool jump;

    private void Awake()
    {
        Reset();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Reset();
        if(Input.GetKey(KeyCode.A))
        {
            left = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            right = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    private void Reset()
    {
        left = false;
        right = false;
        jump = false;
    }

    public bool GetLeft()
    {
        return left;
    }

    public bool GetRight()
    {
        return right;
    }

    public bool GetJump()
    {
        return jump;
    }
}
