using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidPickUp : MonoBehaviour
{
    public int score;
    private int points = 5;
    private bool end = false;

    private void Awake()
    {
        score = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Eye")
        {
            AddScore();
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "Limit")
        {
            end = true;
        }
    }

    public bool GetEnd()
    {
        return end;
    }

    private void AddScore()
    {
        score += points;
    }

    public int GetScore()
    {
        return score;
    }
}
